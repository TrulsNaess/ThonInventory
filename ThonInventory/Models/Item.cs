using System.ComponentModel.DataAnnotations;

namespace ThonInventory.Models;

public class Item
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    // Keeps the same ordering as the current hotel sheet inside each category.
    public int DisplayOrder { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    public bool IsMissing { get; set; }

    public DateTime? MissingSinceDate { get; set; }

    [MaxLength(250)]
    public string? Notes { get; set; }

    public bool IsOrdered { get; set; }

    public DateTime? OrderedAtDate { get; set; }

    public DateTime? ExpectedDeliveryDate { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
}
