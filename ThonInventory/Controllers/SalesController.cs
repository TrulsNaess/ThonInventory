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
    public async Task<ActionResult<SalesLogResponse>> GetSalesLog([FromQuery] string period = "month", CancellationToken cancellationToken = default)
    {
        var normalizedPeriod = period.Trim().ToLowerInvariant();
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

        var entries = await dbContext.SalesLogEntries
            .AsNoTracking()
            .Include(entry => entry.Item)
            .Where(entry => entry.SoldAt >= from)
            .OrderByDescending(entry => entry.SoldAt)
            .Select(entry => new SalesLogEntryResponse(
                entry.Id,
                entry.ItemId,
                entry.Item != null ? entry.Item.Name : "Ukjent vare",
                entry.QuantitySold,
                entry.UnitPrice,
                entry.QuantitySold * entry.UnitPrice,
                entry.SoldAt))
            .ToListAsync(cancellationToken);

        var totalRevenue = entries.Sum(entry => entry.Total);
        var totalUnitsSold = entries.Sum(entry => entry.QuantitySold);

        return Ok(new SalesLogResponse(normalizedPeriod, from, now, totalUnitsSold, totalRevenue, entries));
    }

    [HttpPost]
    public async Task<ActionResult<SalesLogEntryResponse>> RegisterSale([FromBody] RegisterSaleRequest request, CancellationToken cancellationToken)
    {
        if (request.QuantitySold <= 0)
        {
            return ValidationProblem("QuantitySold må være større enn 0.");
        }

        var item = await dbContext.Items.FirstOrDefaultAsync(current => current.Id == request.ItemId, cancellationToken);

        if (item is null)
        {
            return NotFound($"Fant ikke vare med id {request.ItemId}.");
        }

        if (item.Quantity < request.QuantitySold)
        {
            return ValidationProblem($"Ikke nok beholdning for {item.Name}. Lager: {item.Quantity}, forespurt salg: {request.QuantitySold}.");
        }

        item.Quantity -= request.QuantitySold;

        var entry = new SalesLogEntry
        {
            ItemId = item.Id,
            QuantitySold = request.QuantitySold,
            UnitPrice = request.UnitPrice ?? item.Price,
            SoldAt = request.SoldAt ?? DateTime.UtcNow
        };

        dbContext.SalesLogEntries.Add(entry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(new SalesLogEntryResponse(
            entry.Id,
            item.Id,
            item.Name,
            entry.QuantitySold,
            entry.UnitPrice,
            entry.QuantitySold * entry.UnitPrice,
            entry.SoldAt));
    }
}

public record RegisterSaleRequest(
    int ItemId,
    int QuantitySold,
    decimal? UnitPrice,
    DateTime? SoldAt);

public record SalesLogResponse(
    string Period,
    DateTime From,
    DateTime To,
    int TotalUnitsSold,
    decimal TotalRevenue,
    IReadOnlyList<SalesLogEntryResponse> Entries);

public record SalesLogEntryResponse(
    int Id,
    int ItemId,
    string ItemName,
    int QuantitySold,
    decimal UnitPrice,
    decimal Total,
    DateTime SoldAt);
