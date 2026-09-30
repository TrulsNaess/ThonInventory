using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThonInventory.Data;

namespace ThonInventory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController(ApplicationDbContext dbContext) : ControllerBase
{
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
                        item.IsOrdered,
                        item.IsMissing,
                        item.Notes))
                    .ToList()))
            .ToListAsync(cancellationToken);

        return Ok(new InventoryResponse(categories));
    }
}

public record InventoryResponse(IReadOnlyList<CategoryResponse> Categories);

public record CategoryResponse(
    int Id,
    string Name,
    IReadOnlyList<ItemResponse> Items);

public record ItemResponse(
    int Id,
    string Name,
    double Price,
    bool IsOrdered,
    bool IsMissing,
    string? Notes);
