using System.ComponentModel.DataAnnotations;

namespace ThonInventory.Models;

public class SalesLogEntry
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public Item? Item { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    public InventoryTransactionType TransactionType { get; set; }

    [MaxLength(250)]
    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }
}
