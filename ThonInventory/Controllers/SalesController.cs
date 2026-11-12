using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThonInventory.Data;
using ThonInventory.Models;

namespace ThonInventory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController(ApplicationDbContext dbContext) : ControllerBase
{
    private const string ShrinkageCategoryPrefix = "shrinkage:";

    [HttpGet]
    public async Task<ActionResult<SalesLogResponse>> GetSalesLog(
        [FromQuery] string period = "month",
        [FromQuery] string type = "all",
        [FromQuery] string category = "all",
        CancellationToken cancellationToken = default)
    {
        var normalizedPeriod = period.Trim().ToLowerInvariant();
        var normalizedType = type.Trim().ToLowerInvariant();
        var normalizedCategory = category.Trim().ToLowerInvariant();
        var now = DateTime.UtcNow;

        var from = normalizedPeriod switch
        {
            "today" => now.Date,
            "week" => now.Date.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday),
            "month" => new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc),
            _ => throw new ArgumentException("Ugyldig period. Bruk 'today', 'week' eller 'month'.")
        };

        if (normalizedPeriod == "week" && from > now.Date)
        {
            from = from.AddDays(-7);
        }

        var query = dbContext.SalesLogEntries
            .AsNoTracking()
            .Include(entry => entry.Item)
            .Where(entry => entry.CreatedAt >= from);

        query = normalizedType switch
        {
            "sale" => query.Where(entry => entry.TransactionType == InventoryTransactionType.Sale),
            "restock" => query.Where(entry => entry.TransactionType == InventoryTransactionType.Restock),
            "shrinkage" => query.Where(entry => entry.TransactionType == InventoryTransactionType.Shrinkage),
            "order" => query.Where(entry => entry.TransactionType == InventoryTransactionType.OrderPlaced || entry.TransactionType == InventoryTransactionType.OrderConfirmed || entry.TransactionType == InventoryTransactionType.OrderRejected),
            "orderplaced" => query.Where(entry => entry.TransactionType == InventoryTransactionType.OrderPlaced),
            "orderconfirmed" => query.Where(entry => entry.TransactionType == InventoryTransactionType.OrderConfirmed),
            "orderrejected" => query.Where(entry => entry.TransactionType == InventoryTransactionType.OrderRejected),
            "all" => query,
            _ => throw new ArgumentException("Ugyldig type.")
        };

        query = normalizedCategory switch
        {
            "given_away" => query.Where(entry => entry.TransactionType == InventoryTransactionType.Shrinkage && entry.Note != null && entry.Note.StartsWith($"{ShrinkageCategoryPrefix}given_away|")),
            "misregistered" => query.Where(entry => entry.TransactionType == InventoryTransactionType.Shrinkage && entry.Note != null && entry.Note.StartsWith($"{ShrinkageCategoryPrefix}misregistered|")),
            "missing_or_stolen" => query.Where(entry => entry.TransactionType == InventoryTransactionType.Shrinkage && entry.Note != null && entry.Note.StartsWith($"{ShrinkageCategoryPrefix}missing_or_stolen|")),
            "orderplaced" => query.Where(entry => entry.TransactionType == InventoryTransactionType.OrderPlaced),
            "orderconfirmed" => query.Where(entry => entry.TransactionType == InventoryTransactionType.OrderConfirmed),
            "orderrejected" => query.Where(entry => entry.TransactionType == InventoryTransactionType.OrderRejected),
            "all" => query,
            _ => query
        };

        var entries = await query
            .OrderByDescending(entry => entry.CreatedAt)
            .Select(entry => ToResponse(entry, entry.Item != null ? entry.Item.Name : "Ukjent vare"))
            .ToListAsync(cancellationToken);

        var saleEntries = entries.Where(entry => entry.Type == "sale").ToList();
        var shrinkageEntries = entries.Where(entry => entry.Type == "shrinkage").ToList();
        var restockEntries = entries.Where(entry => entry.Type == "restock").ToList();

        return Ok(new SalesLogResponse(
            normalizedPeriod,
            normalizedType,
            normalizedCategory,
            from,
            now,
            saleEntries.Sum(entry => entry.Quantity),
            saleEntries.Sum(entry => entry.Total),
            shrinkageEntries.Sum(entry => entry.Total),
            restockEntries.Sum(entry => entry.Total),
            entries));
    }

    [HttpPost]
    public async Task<ActionResult<SalesLogEntryResponse>> RegisterSale([FromBody] RegisterSaleRequest request, CancellationToken cancellationToken)
    {
        var item = await ValidateItemAndQuantity(request.ItemId, request.Quantity, cancellationToken);

        if (item is null)
        {
            return Problem("Ugyldig vare eller antall.");
        }

        item.Quantity -= request.Quantity;

        var entry = new SalesLogEntry
        {
            ItemId = item.Id,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice ?? item.Price,
            TransactionType = InventoryTransactionType.Sale,
            Note = request.Note,
            CreatedAt = request.CreatedAt ?? DateTime.UtcNow
        };

        dbContext.SalesLogEntries.Add(entry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(ToResponse(entry, item.Name));
    }

    [HttpPost("restock")]
    public async Task<ActionResult<SalesLogEntryResponse>> RegisterRestock([FromBody] RegisterInventoryChangeRequest request, CancellationToken cancellationToken)
    {
        var item = await ValidateItemAndQuantity(request.ItemId, request.Quantity, cancellationToken, skipStockValidation: true);

        if (item is null)
        {
            return Problem("Ugyldig vare eller antall.");
        }

        item.Quantity += request.Quantity;
        item.IsMissing = false;
        item.MissingSinceDate = null;

        var entry = new SalesLogEntry
        {
            ItemId = item.Id,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice ?? item.Price,
            TransactionType = InventoryTransactionType.Restock,
            Note = request.Note,
            CreatedAt = request.CreatedAt ?? DateTime.UtcNow
        };

        dbContext.SalesLogEntries.Add(entry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(ToResponse(entry, item.Name));
    }

    [HttpPost("shrinkage")]
    public async Task<ActionResult<SalesLogEntryResponse>> RegisterShrinkage([FromBody] RegisterInventoryChangeRequest request, CancellationToken cancellationToken)
    {
        var item = await ValidateItemAndQuantity(request.ItemId, request.Quantity, cancellationToken);

        if (item is null)
        {
            return Problem("Ugyldig vare eller antall.");
        }

        item.Quantity -= request.Quantity;

        if (item.Quantity == 0)
        {
            item.IsMissing = true;
            item.MissingSinceDate = DateTime.UtcNow;
        }

        var entry = new SalesLogEntry
        {
            ItemId = item.Id,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice ?? item.Price,
            TransactionType = InventoryTransactionType.Shrinkage,
            Note = EncodeShrinkageNote(request.Category, request.Note),
            CreatedAt = request.CreatedAt ?? DateTime.UtcNow
        };

        dbContext.SalesLogEntries.Add(entry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(ToResponse(entry, item.Name));
    }

    [HttpPost("order/register")]
    public async Task<ActionResult<SalesLogEntryResponse>> RegisterOrder([FromBody] RegisterOrderRequest request, CancellationToken cancellationToken)
    {
        var item = await dbContext.Items.FirstOrDefaultAsync(current => current.Id == request.ItemId, cancellationToken);

        if (item is null || request.Quantity <= 0)
        {
            return Problem("Ugyldig vare eller antall.");
        }

        var createdAt = request.CreatedAt ?? DateTime.UtcNow;

        item.IsOrdered = true;
        item.OrderedQuantity = request.Quantity;
        item.OrderedAtDate = createdAt;
        item.ExpectedDeliveryDate = request.ExpectedDeliveryDate;

        var entry = new SalesLogEntry
        {
            ItemId = item.Id,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice ?? item.Price,
            TransactionType = InventoryTransactionType.OrderPlaced,
            Note = request.Note,
            CreatedAt = createdAt
        };

        dbContext.SalesLogEntries.Add(entry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(ToResponse(entry, item.Name));
    }

    [HttpPost("order/confirm")]
    public async Task<ActionResult<SalesLogEntryResponse>> ConfirmOrder([FromBody] ConfirmOrderRequest request, CancellationToken cancellationToken)
    {
        var item = await dbContext.Items.FirstOrDefaultAsync(current => current.Id == request.ItemId, cancellationToken);

        if (item is null || !item.IsOrdered || request.ReceivedQuantity <= 0)
        {
            return Problem("Ugyldig bestilling eller antall.");
        }

        item.Quantity += request.ReceivedQuantity;
        item.IsMissing = false;
        item.MissingSinceDate = null;
        item.IsOrdered = false;
        item.OrderedQuantity = 0;
        item.OrderedAtDate = null;
        item.ExpectedDeliveryDate = null;

        var entry = new SalesLogEntry
        {
            ItemId = item.Id,
            Quantity = request.ReceivedQuantity,
            UnitPrice = item.Price,
            TransactionType = InventoryTransactionType.OrderConfirmed,
            Note = request.Note,
            CreatedAt = request.CreatedAt ?? DateTime.UtcNow
        };

        dbContext.SalesLogEntries.Add(entry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(ToResponse(entry, item.Name));
    }

    [HttpPost("order/reject")]
    public async Task<ActionResult<SalesLogEntryResponse>> RejectOrder([FromBody] RejectOrderRequest request, CancellationToken cancellationToken)
    {
        var item = await dbContext.Items.FirstOrDefaultAsync(current => current.Id == request.ItemId, cancellationToken);

        if (item is null || !item.IsOrdered)
        {
            return Problem("Fant ingen aktiv bestilling.");
        }

        var rejectedQuantity = item.OrderedQuantity > 0 ? item.OrderedQuantity : 1;

        item.IsOrdered = false;
        item.OrderedQuantity = 0;
        item.OrderedAtDate = null;
        item.ExpectedDeliveryDate = null;

        var entry = new SalesLogEntry
        {
            ItemId = item.Id,
            Quantity = rejectedQuantity,
            UnitPrice = item.Price,
            TransactionType = InventoryTransactionType.OrderRejected,
            Note = request.Note,
            CreatedAt = request.CreatedAt ?? DateTime.UtcNow
        };

        dbContext.SalesLogEntries.Add(entry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(ToResponse(entry, item.Name));
    }

    [HttpPost("demo-checkout")]
    public async Task<ActionResult<DemoCheckoutResponse>> DemoCheckout([FromBody] DemoCheckoutRequest request, CancellationToken cancellationToken)
    {
        if (request.Lines.Count == 0)
        {
            return ValidationProblem("Handlekurven kan ikke være tom.");
        }

        var itemIds = request.Lines.Select(line => line.ItemId).Distinct().ToList();
        var items = await dbContext.Items.Where(item => itemIds.Contains(item.Id)).ToDictionaryAsync(item => item.Id, cancellationToken);

        foreach (var line in request.Lines)
        {
            if (line.Quantity <= 0)
            {
                return ValidationProblem("Antall må være større enn 0 for alle linjer.");
            }

            if (!items.TryGetValue(line.ItemId, out var item))
            {
                return NotFound($"Fant ikke vare med id {line.ItemId}.");
            }

            if (item.Quantity < line.Quantity)
            {
                return ValidationProblem($"Ikke nok beholdning for {item.Name}. Lager: {item.Quantity}, forespurt salg: {line.Quantity}.");
            }
        }

        var createdAt = request.CreatedAt ?? DateTime.UtcNow;
        var entries = new List<SalesLogEntry>();

        foreach (var line in request.Lines)
        {
            var item = items[line.ItemId];
            item.Quantity -= line.Quantity;

            var entry = new SalesLogEntry
            {
                ItemId = item.Id,
                Quantity = line.Quantity,
                UnitPrice = line.UnitPrice ?? item.Price,
                TransactionType = InventoryTransactionType.Sale,
                Note = request.Note,
                CreatedAt = createdAt
            };

            entries.Add(entry);
        }

        dbContext.SalesLogEntries.AddRange(entries);
        await dbContext.SaveChangesAsync(cancellationToken);

        var responseEntries = entries
            .Select(entry => ToResponse(entry, items[entry.ItemId].Name))
            .ToList();

        return Ok(new DemoCheckoutResponse(responseEntries.Sum(entry => entry.Total), responseEntries));
    }

    private async Task<Item?> ValidateItemAndQuantity(int itemId, int quantity, CancellationToken cancellationToken, bool skipStockValidation = false)
    {
        if (quantity <= 0)
        {
            return null;
        }

        var item = await dbContext.Items.FirstOrDefaultAsync(current => current.Id == itemId, cancellationToken);

        if (item is null)
        {
            return null;
        }

        if (!skipStockValidation && item.Quantity < quantity)
        {
            return null;
        }

        return item;
    }

    private static SalesLogEntryResponse ToResponse(SalesLogEntry entry, string itemName)
    {
        var (category, note) = ParseCategoryAndNote(entry.TransactionType, entry.Note);

        return new SalesLogEntryResponse(
            entry.Id,
            entry.ItemId,
            itemName,
            entry.Quantity,
            entry.UnitPrice,
            entry.Quantity * entry.UnitPrice,
            entry.TransactionType.ToString().ToLowerInvariant(),
            category,
            note,
            entry.CreatedAt);
    }

    private static string EncodeShrinkageNote(string? category, string? note)
    {
        var normalizedCategory = NormalizeShrinkageCategory(category);

        return $"{ShrinkageCategoryPrefix}{normalizedCategory}|{note ?? string.Empty}";
    }

    private static (string Category, string? Note) ParseCategoryAndNote(InventoryTransactionType transactionType, string? note)
    {
        if (transactionType == InventoryTransactionType.Shrinkage)
        {
            if (!string.IsNullOrWhiteSpace(note) && note.StartsWith(ShrinkageCategoryPrefix, StringComparison.OrdinalIgnoreCase))
            {
                var payload = note[ShrinkageCategoryPrefix.Length..];
                var separatorIndex = payload.IndexOf('|');

                if (separatorIndex >= 0)
                {
                    var category = payload[..separatorIndex];
                    var cleanNote = payload[(separatorIndex + 1)..];
                    return (category, string.IsNullOrWhiteSpace(cleanNote) ? null : cleanNote);
                }

                return (payload, null);
            }

            return ("uncategorized", note);
        }

        var transactionCategory = transactionType.ToString().ToLowerInvariant();
        return (transactionCategory, note);
    }

    private static string NormalizeShrinkageCategory(string? category)
    {
        return category?.Trim().ToLowerInvariant() switch
        {
            "given_away" => "given_away",
            "misregistered" => "misregistered",
            "missing_or_stolen" => "missing_or_stolen",
            _ => "misregistered"
        };
    }
}

public record RegisterSaleRequest(
    int ItemId,
    int Quantity,
    decimal? UnitPrice,
    string? Note,
    DateTime? CreatedAt);

public record RegisterInventoryChangeRequest(
    int ItemId,
    int Quantity,
    decimal? UnitPrice,
    string? Category,
    string? Note,
    DateTime? CreatedAt);

public record RegisterOrderRequest(
    int ItemId,
    int Quantity,
    DateTime? ExpectedDeliveryDate,
    decimal? UnitPrice,
    string? Note,
    DateTime? CreatedAt);

public record ConfirmOrderRequest(
    int ItemId,
    int ReceivedQuantity,
    string? Note,
    DateTime? CreatedAt);

public record RejectOrderRequest(
    int ItemId,
    string? Note,
    DateTime? CreatedAt);

public record DemoCheckoutRequest(
    IReadOnlyList<DemoCheckoutLineRequest> Lines,
    string? Note,
    DateTime? CreatedAt);

public record DemoCheckoutLineRequest(
    int ItemId,
    int Quantity,
    decimal? UnitPrice);

public record DemoCheckoutResponse(
    decimal Total,
    IReadOnlyList<SalesLogEntryResponse> Entries);

public record SalesLogResponse(
    string Period,
    string Type,
    string Category,
    DateTime From,
    DateTime To,
    int TotalUnitsSold,
    decimal TotalRevenue,
    decimal TotalShrinkageLoss,
    decimal TotalRestockValue,
    IReadOnlyList<SalesLogEntryResponse> Entries);

public record SalesLogEntryResponse(
    int Id,
    int ItemId,
    string ItemName,
    int Quantity,
    decimal UnitPrice,
    decimal Total,
    string Type,
    string Category,
    string? Note,
    DateTime CreatedAt);
