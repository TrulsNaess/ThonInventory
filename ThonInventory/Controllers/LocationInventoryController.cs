using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThonInventory.Data;

namespace ThonInventory.Controllers;

[ApiController]
[Route("api/location-inventory")]
public class LocationInventoryController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet("{location}")]
    public async Task<ActionResult<IReadOnlyList<LocationInventoryItemResponse>>> GetInventory(
        string location,
        CancellationToken cancellationToken)
    {
        var categoryName = GetCategoryName(location);

        if (categoryName is null)
        {
            return NotFound();
        }

        var items = await dbContext.Items
            .AsNoTracking()
            .Where(item =>
                item.Category != null &&
                item.Category.Name == categoryName)
            .OrderBy(item => item.DisplayOrder)
            .Select(item => new LocationInventoryItemResponse(
                item.Id,
                item.Name,
                item.Quantity))
            .ToListAsync(cancellationToken);

        return Ok(items);
    }

    [HttpPost("{location}/{itemId:int}/adjust")]
    public async Task<ActionResult<LocationInventoryItemResponse>> AdjustQuantity(
        string location,
        int itemId,
        [FromBody] AdjustQuantityRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Delta != -1 && request.Delta != 1)
        {
            return BadRequest("Delta må være -1 eller 1.");
        }

        var categoryName = GetCategoryName(location);

        if (categoryName is null)
        {
            return NotFound();
        }

        var item = await dbContext.Items
            .Include(item => item.Category)
            .FirstOrDefaultAsync(
                item =>
                    item.Id == itemId &&
                    item.Category != null &&
                    item.Category.Name == categoryName,
                cancellationToken);

        if (item is null)
        {
            return NotFound();
        }

        var newQuantity = item.Quantity + request.Delta;

        if (newQuantity < 0)
        {
            return BadRequest("Antall kan ikke være lavere enn 0.");
        }

        item.Quantity = newQuantity;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(new LocationInventoryItemResponse(
            item.Id,
            item.Name,
            item.Quantity));
    }

    private static string? GetCategoryName(string location)
    {
        return location.ToLowerInvariant() switch
        {
            "maintenance" => "MAINTENANCE",
            "resepsjon" => "RESEPSJON",
            "backoffice" => "BACKOFFICE",
            _ => null
        };
    }
}

public record LocationInventoryItemResponse(
    int Id,
    string Name,
    int Quantity);

public record AdjustQuantityRequest(
    int Delta);