namespace ThonInventory.Models;

public enum InventoryTransactionType
{
    Sale = 0,
    Restock = 1,
    Shrinkage = 2,
    OrderPlaced = 3,
    OrderConfirmed = 4,
    OrderRejected = 5
}
