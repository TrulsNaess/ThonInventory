using System.ComponentModel.DataAnnotations;

namespace ThonInventory.Models;

public class SalesLogEntry
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public Item? Item { get; set; }

    [Range(1, int.MaxValue)]
    public int QuantitySold { get; set; }

    [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    public DateTime SoldAt { get; set; }
}
