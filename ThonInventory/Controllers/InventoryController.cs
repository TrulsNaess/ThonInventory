using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThonInventory.Data;

namespace ThonInventory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController(ApplicationDbContext dbContext) : ControllerBase
{
    private const int LowStockThreshold = 30;

    [HttpGet]
    public async Task<ActionResult<InventoryResponse>> GetInventory(CancellationToken cancellationToken)
    {
        var categories = await dbContext.Categories
            .AsNoTracking()
            .OrderBy(category => category.DisplayOrder)
            .Select(category => new CategoryResponse(
                category.Id,
                category.Name,
                category.Items
                    .OrderBy(item => item.DisplayOrder)
                    .Select(item => new ItemResponse(
                        item.Id,
                        item.Name,
                        item.Price,
                        item.Quantity,
                        item.Price * item.Quantity,
                        item.IsOrdered,
                        item.IsMissing,
                        item.MissingSinceDate,
                        item.OrderedAtDate,
                        item.ExpectedDeliveryDate,
                        item.Notes))
                    .ToList()))
            .ToListAsync(cancellationToken);

        var allItems = categories.SelectMany(category => category.Items).ToList();
        var summary = new InventorySummaryResponse(
            allItems.Count,
            allItems.Sum(item => item.Quantity),
            allItems.Count(item => item.IsMissing),
            allItems.Count(item => item.IsOrdered),
            allItems.Sum(item => item.StockValue));

        var alerts = allItems
            .Where(item => item.Quantity < LowStockThreshold)
            .OrderBy(item => item.Quantity)
            .Select(item => new LowStockAlertResponse(
                item.Id,
                item.Name,
                item.Quantity,
                LowStockThreshold,
                $"{item.Name} har lav beholdning ({item.Quantity})."))
            .ToList();

        return Ok(new InventoryResponse(categories, summary, alerts));
    }
}

public record InventoryResponse(
    IReadOnlyList<CategoryResponse> Categories,
    InventorySummaryResponse Summary,
    IReadOnlyList<LowStockAlertResponse> Alerts);

public record InventorySummaryResponse(
    int TotalItems,
    int TotalQuantity,
    int MissingCount,
    int OrderedCount,
    decimal TotalStockValue);

public record LowStockAlertResponse(
    int ItemId,
    string ItemName,
    int Quantity,
    int Threshold,
    string Message);

public record CategoryResponse(
    int Id,
    string Name,
    IReadOnlyList<ItemResponse> Items);

public record ItemResponse(
    int Id,
    string Name,
    decimal Price,
    int Quantity,
    decimal StockValue,
    bool IsOrdered,
    bool IsMissing,
    DateTime? MissingSinceDate,
    DateTime? OrderedAtDate,
    DateTime? ExpectedDeliveryDate,
    string? Notes);
