using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThonInventory.Data;
using ThonInventory.Models;

namespace ThonInventory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<SalesLogResponse>> GetSalesLog(
        [FromQuery] string period = "month",
        [FromQuery] string type = "all",
        CancellationToken cancellationToken = default)
    {
        var normalizedPeriod = period.Trim().ToLowerInvariant();
        var normalizedType = type.Trim().ToLowerInvariant();
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
            "all" => query,
            _ => throw new ArgumentException("Ugyldig type. Bruk 'all', 'sale', 'restock' eller 'shrinkage'.")
        };

        var entries = await query
            .OrderByDescending(entry => entry.CreatedAt)
            .Select(entry => new SalesLogEntryResponse(
                entry.Id,
                entry.ItemId,
                entry.Item != null ? entry.Item.Name : "Ukjent vare",
                entry.Quantity,
                entry.UnitPrice,
                entry.Quantity * entry.UnitPrice,
                entry.TransactionType.ToString().ToLowerInvariant(),
                entry.Note,
                entry.CreatedAt))
            .ToListAsync(cancellationToken);

        var saleEntries = entries.Where(entry => entry.Type == "sale").ToList();
        var shrinkageEntries = entries.Where(entry => entry.Type == "shrinkage").ToList();
        var restockEntries = entries.Where(entry => entry.Type == "restock").ToList();

        return Ok(new SalesLogResponse(
            normalizedPeriod,
            normalizedType,
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

    private static SalesLogEntryResponse ToResponse(SalesLogEntry entry, string itemName) =>
        new(
            entry.Id,
            entry.ItemId,
            itemName,
            entry.Quantity,
            entry.UnitPrice,
            entry.Quantity * entry.UnitPrice,
            entry.TransactionType.ToString().ToLowerInvariant(),
            entry.Note,
            entry.CreatedAt);
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
    string? Note,
    DateTime CreatedAt);
