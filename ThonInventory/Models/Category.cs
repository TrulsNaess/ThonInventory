using System.ComponentModel.DataAnnotations;

namespace ThonInventory.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    // Keeps the same ordering as the current hotel sheet.
    public int DisplayOrder { get; set; }

    public ICollection<Item> Items { get; set; } = new List<Item>();
}
