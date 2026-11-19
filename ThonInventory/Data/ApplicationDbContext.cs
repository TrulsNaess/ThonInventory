using Microsoft.EntityFrameworkCore;
using ThonInventory.Models;

namespace ThonInventory.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<SalesLogEntry> SalesLogEntries => Set<SalesLogEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasIndex(category => category.Name)
            .IsUnique();

        modelBuilder.Entity<Item>()
            .HasIndex(item => new { item.CategoryId, item.Name })
            .IsUnique();

        modelBuilder.Entity<Item>()
            .HasOne(item => item.Category)
            .WithMany(category => category.Items)
            .HasForeignKey(item => item.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Item>()
            .Property(item => item.Price)
            .HasColumnType("numeric(10,2)");

        modelBuilder.Entity<SalesLogEntry>()
            .HasOne(entry => entry.Item)
            .WithMany()
            .HasForeignKey(entry => entry.ItemId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SalesLogEntry>()
            .Property(entry => entry.UnitPrice)
            .HasColumnType("numeric(10,2)");

        modelBuilder.Entity<SalesLogEntry>()
            .HasIndex(entry => entry.CreatedAt);

        SeedSheetData(modelBuilder);
    }

    private static void SeedSheetData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
    new Category { Id = 1, Name = "BRUS", DisplayOrder = 1 },
    new Category { Id = 2, Name = "SNACKS", DisplayOrder = 2 },
    new Category { Id = 3, Name = "ALKOHOL", DisplayOrder = 3 },
    new Category { Id = 4, Name = "KIOSK", DisplayOrder = 4 },
    new Category { Id = 5, Name = "RESEPSJON", DisplayOrder = 5 },
    new Category { Id = 6, Name = "BACKOFFICE", DisplayOrder = 6 },
    new Category { Id = 7, Name = "MAINTENANCE", DisplayOrder = 7 });

        modelBuilder.Entity<Item>().HasData(CreateSeedItems());
        modelBuilder.Entity<SalesLogEntry>().HasData(CreateSeedSalesEntries());
    }

    private static Item[] CreateSeedItems() =>
    [
        new() { Id = 1, CategoryId = 1, Name = "Coca-Cola", DisplayOrder = 1, Quantity = 48, Price = 58m },
        new() { Id = 2, CategoryId = 1, Name = "Coca-Cola Zero", DisplayOrder = 2, Quantity = 44, Price = 58m },
        new() { Id = 3, CategoryId = 1, Name = "Fanta", DisplayOrder = 3, Quantity = 26, Price = 58m, IsOrdered = true, OrderedQuantity = 12, OrderedAtDate = new DateTime(2026, 10, 27, 9, 0, 0, DateTimeKind.Utc), ExpectedDeliveryDate = new DateTime(2026, 10, 30, 11, 0, 0, DateTimeKind.Utc) },
        new() { Id = 4, CategoryId = 1, Name = "Sprite", DisplayOrder = 4, Quantity = 31, Price = 58m },
        new() { Id = 5, CategoryId = 1, Name = "Eplemost", DisplayOrder = 5, Quantity = 14, Price = 89m, IsOrdered = true, OrderedQuantity = 8, OrderedAtDate = new DateTime(2026, 10, 27, 9, 5, 0, DateTimeKind.Utc), ExpectedDeliveryDate = new DateTime(2026, 10, 31, 12, 0, 0, DateTimeKind.Utc) },
        new() { Id = 6, CategoryId = 1, Name = "Mineralvann", DisplayOrder = 6, Quantity = 23, Price = 58m },
        new() { Id = 7, CategoryId = 1, Name = "Mineralvann med kullsyre", DisplayOrder = 7, Quantity = 19, Price = 58m },
        new() { Id = 8, CategoryId = 1, Name = "Red Bull", DisplayOrder = 8, Quantity = 12, Price = 39m },
        new() { Id = 9, CategoryId = 1, Name = "Monster", DisplayOrder = 9, Quantity = 10, Price = 68m },

        new() { Id = 10, CategoryId = 2, Name = "Stratos", DisplayOrder = 1, Quantity = 51, Price = 59m },
        new() { Id = 11, CategoryId = 2, Name = "Melkesjokolade", DisplayOrder = 2, Quantity = 39, Price = 59m },
        new() { Id = 12, CategoryId = 2, Name = "Sørlandschips", DisplayOrder = 3, Quantity = 22, Price = 59m, IsOrdered = true, OrderedQuantity = 10, OrderedAtDate = new DateTime(2026, 10, 25, 13, 0, 0, DateTimeKind.Utc), ExpectedDeliveryDate = new DateTime(2026, 10, 29, 10, 0, 0, DateTimeKind.Utc) },
        new() { Id = 13, CategoryId = 2, Name = "Sørlandschips (små poser)", DisplayOrder = 4, Quantity = 28, Price = 0m },
        new() { Id = 14, CategoryId = 2, Name = "Chillinøtter", DisplayOrder = 5, Quantity = 18, Price = 42m },
        new() { Id = 15, CategoryId = 2, Name = "Peanøtter", DisplayOrder = 6, Quantity = 16, Price = 42m },
        new() { Id = 16, CategoryId = 2, Name = "Polly Nøtter", DisplayOrder = 7, Quantity = 27, Price = 42m },
        new() { Id = 17, CategoryId = 2, Name = "Extra tyggis", DisplayOrder = 8, Quantity = 60, Price = 32m },
        new() { Id = 18, CategoryId = 2, Name = "Små sulten", DisplayOrder = 9, Quantity = 18, Price = 38m },

        new() { Id = 20, CategoryId = 3, Name = "Carlsberg", DisplayOrder = 1, Quantity = 47, Price = 115m },
        new() { Id = 21, CategoryId = 3, Name = "Alkoholfri øl", DisplayOrder = 2, Quantity = 36, Price = 79m },
        new() { Id = 43, CategoryId = 3, Name = "Somersby Cider", DisplayOrder = 3, Quantity = 0, Price = 124m },
        new() { Id = 22, CategoryId = 3, Name = "Gin-Tonic i boks", DisplayOrder = 4, Quantity = 25, Price = 115m },
        new() { Id = 23, CategoryId = 3, Name = "Bulmers Cider 0.5l", DisplayOrder = 5, Quantity = 17, Price = 159m },

        new() { Id = 24, CategoryId = 3, Name = "Prosecco 375ml (små flaske)", DisplayOrder = 6, Quantity = 34, Price = 297m },
        new() { Id = 25, CategoryId = 3, Name = "Prosecco 750ml", DisplayOrder = 7, Quantity = 12, Price = 624m, IsOrdered = true, OrderedQuantity = 6, OrderedAtDate = new DateTime(2026, 10, 24, 14, 30, 0, 0, DateTimeKind.Utc), ExpectedDeliveryDate = new DateTime(2026, 10, 30, 14, 0, 0, DateTimeKind.Utc) },
        new() { Id = 26, CategoryId = 3, Name = "Rødvin 37.5cl (små flaske)", DisplayOrder = 8, Quantity = 20, Price = 315m },
        new() { Id = 27, CategoryId = 3, Name = "Rødvin 750ml", DisplayOrder = 9, Quantity = 15, Price = 572m },
        new() { Id = 28, CategoryId = 3, Name = "Hvitvin 375ml (små flaske)", DisplayOrder = 10, Quantity = 16, Price = 315m },
        new() { Id = 29, CategoryId = 3, Name = "Hvitvin 750ml", DisplayOrder = 11, Quantity = 11, Price = 572m },

        new() { Id = 30, CategoryId = 3, Name = "Jameson", DisplayOrder = 12, Quantity = 8, Price = 126m, IsOrdered = true, OrderedQuantity = 5, OrderedAtDate = new DateTime(2026, 10, 22, 15, 0, 0, DateTimeKind.Utc), ExpectedDeliveryDate = new DateTime(2026, 10, 31, 12, 0, 0, DateTimeKind.Utc) },
        new() { Id = 31, CategoryId = 3, Name = "Rom&Cola", DisplayOrder = 13, Quantity = 9, Price = 167m },
        new() { Id = 32, CategoryId = 3, Name = "Cognac", DisplayOrder = 14, Quantity = 4, Price = 126m },
        new() { Id = 33, CategoryId = 3, Name = "Gin", DisplayOrder = 15, Quantity = 7, Price = 126m },
        new() { Id = 34, CategoryId = 3, Name = "Pink Gin", DisplayOrder = 16, Quantity = 6, Price = 126m },
        new() { Id = 35, CategoryId = 3, Name = "Gin&Tonic", DisplayOrder = 17, Quantity = 33, Price = 167m },

        new() { Id = 36, CategoryId = 4, Name = "Tannbørste", DisplayOrder = 1, Quantity = 14, Price = 47m },
        new() { Id = 37, CategoryId = 4, Name = "Tannkrem", DisplayOrder = 2, Quantity = 18, Price = 42m },
        new() { Id = 44, CategoryId = 4, Name = "Shaving kit", DisplayOrder = 3, Quantity = 0, Price = 58m },
        new() { Id = 45, CategoryId = 4, Name = "Paraply", DisplayOrder = 4, Quantity = 0, Price = 74m },
        new() { Id = 38, CategoryId = 4, Name = "OB rosa", DisplayOrder = 5, Quantity = 22, Price = 37m },
        new() { Id = 39, CategoryId = 4, Name = "OB blå", DisplayOrder = 6, Quantity = 20, Price = 37m },
        new() { Id = 40, CategoryId = 4, Name = "Bind", DisplayOrder = 7, Quantity = 13, Price = 42m },
        new() { Id = 41, CategoryId = 4, Name = "Kortstokk", DisplayOrder = 8, Quantity = 9, Price = 49m },
        new() { Id = 42, CategoryId = 4, Name = "Yatzy", DisplayOrder = 9, Quantity = 7, Price = 79m },

// Resepsjonslager
new() { Id = 100, CategoryId = 5, Name = "Varmtvannskokere", DisplayOrder = 1, Quantity = 3, Price = 0m },
new() { Id = 101, CategoryId = 5, Name = "Grønn te", DisplayOrder = 2, Quantity = 48, Price = 0m },
new() { Id = 102, CategoryId = 5, Name = "Rød te", DisplayOrder = 3, Quantity = 42, Price = 0m },
new() { Id = 103, CategoryId = 5, Name = "Gul te", DisplayOrder = 4, Quantity = 28, Price = 0m },
new() { Id = 104, CategoryId = 5, Name = "Oransje te", DisplayOrder = 5, Quantity = 25, Price = 0m },
new() { Id = 105, CategoryId = 5, Name = "Kakao", DisplayOrder = 6, Quantity = 36, Price = 0m },
new() { Id = 106, CategoryId = 5, Name = "Kaffe", DisplayOrder = 7, Quantity = 60, Price = 0m },
new() { Id = 107, CategoryId = 5, Name = "Sørlandschips", DisplayOrder = 8, Quantity = 20, Price = 0m },
new() { Id = 108, CategoryId = 5, Name = "Tørkepapir", DisplayOrder = 9, Quantity = 12, Price = 0m },
new() { Id = 109, CategoryId = 5, Name = "Hårfønere", DisplayOrder = 10, Quantity = 7, Price = 0m },
new() { Id = 110, CategoryId = 5, Name = "Vifter", DisplayOrder = 11, Quantity = 5, Price = 0m },
new() { Id = 111, CategoryId = 5, Name = "Kopper", DisplayOrder = 12, Quantity = 80, Price = 0m },
new() { Id = 112, CategoryId = 5, Name = "Ovner", DisplayOrder = 13, Quantity = 2, Price = 0m },
new() { Id = 113, CategoryId = 5, Name = "Poser", DisplayOrder = 14, Quantity = 150, Price = 0m },
new() { Id = 114, CategoryId = 5, Name = "Dopapir", DisplayOrder = 15, Quantity = 30, Price = 0m },
new() { Id = 115, CategoryId = 5, Name = "Ark", DisplayOrder = 16, Quantity = 22, Price = 0m },
new() { Id = 116, CategoryId = 5, Name = "Skjøteledninger", DisplayOrder = 17, Quantity = 6, Price = 0m },
new() { Id = 117, CategoryId = 5, Name = "Notatark", DisplayOrder = 18, Quantity = 35, Price = 0m },
new() { Id = 118, CategoryId = 5, Name = "Kulepenner", DisplayOrder = 19, Quantity = 70, Price = 0m },

// Backoffice lager
new() { Id = 120, CategoryId = 6, Name = "Håndkler (store)", DisplayOrder = 1, Quantity = 45, Price = 0m },
new() { Id = 121, CategoryId = 6, Name = "Håndkler (små)", DisplayOrder = 2, Quantity = 70, Price = 0m },
new() { Id = 122, CategoryId = 6, Name = "Ørepropper", DisplayOrder = 3, Quantity = 120, Price = 0m },
new() { Id = 123, CategoryId = 6, Name = "Såpe", DisplayOrder = 4, Quantity = 90, Price = 0m },
new() { Id = 124, CategoryId = 6, Name = "Puter", DisplayOrder = 5, Quantity = 16, Price = 0m },
new() { Id = 125, CategoryId = 6, Name = "Dopapir", DisplayOrder = 6, Quantity = 64, Price = 0m },
new() { Id = 126, CategoryId = 6, Name = "Klesruller", DisplayOrder = 7, Quantity = 18, Price = 0m },
new() { Id = 127, CategoryId = 6, Name = "Tørkepapir", DisplayOrder = 8, Quantity = 25, Price = 0m },
new() { Id = 128, CategoryId = 6, Name = "Flipflopper / sandaler", DisplayOrder = 9, Quantity = 22, Price = 0m },
new() { Id = 129, CategoryId = 6, Name = "Tannkremer", DisplayOrder = 10, Quantity = 50, Price = 0m },
new() { Id = 130, CategoryId = 6, Name = "Antibac", DisplayOrder = 11, Quantity = 14, Price = 0m },
new() { Id = 131, CategoryId = 6, Name = "Blekk til printer", DisplayOrder = 12, Quantity = 9, Price = 0m },
new() { Id = 132, CategoryId = 6, Name = "Ansiktsmasker", DisplayOrder = 13, Quantity = 40, Price = 0m },
new() { Id = 133, CategoryId = 6, Name = "Ekstra ovn", DisplayOrder = 14, Quantity = 2, Price = 0m },
new() { Id = 134, CategoryId = 6, Name = "Fluesmekker", DisplayOrder = 15, Quantity = 11, Price = 0m },

// Maintenance lager
new() { Id = 140, CategoryId = 7, Name = "Batteri AA", DisplayOrder = 1, Quantity = 42, Price = 0m },
new() { Id = 141, CategoryId = 7, Name = "Batteri AAA", DisplayOrder = 2, Quantity = 38, Price = 0m },
new() { Id = 142, CategoryId = 7, Name = "Fjernkontroller", DisplayOrder = 3, Quantity = 6, Price = 0m },
new() { Id = 143, CategoryId = 7, Name = "Ørepropper", DisplayOrder = 4, Quantity = 20, Price = 0m },
new() { Id = 144, CategoryId = 7, Name = "Hammer", DisplayOrder = 5, Quantity = 0, Price = 0m },
new() { Id = 145, CategoryId = 7, Name = "Skrutrekker", DisplayOrder = 6, Quantity = 0, Price = 0m },
new() { Id = 146, CategoryId = 7, Name = "Vanlig verktøy", DisplayOrder = 7, Quantity = 4, Price = 0m },
new() { Id = 147, CategoryId = 7, Name = "Spikere", DisplayOrder = 8, Quantity = 300, Price = 0m },
new() { Id = 148, CategoryId = 7, Name = "Lyspærer", DisplayOrder = 9, Quantity = 28, Price = 0m },
new() { Id = 149, CategoryId = 7, Name = "Nøkkelutstyr", DisplayOrder = 10, Quantity = 14, Price = 0m },
new() { Id = 150, CategoryId = 7, Name = "Bånd", DisplayOrder = 11, Quantity = 16, Price = 0m }
];

    private static SalesLogEntry[] CreateSeedSalesEntries() =>
    [
        new() { Id = 1, ItemId = 1, Quantity = 3, UnitPrice = 58m, TransactionType = InventoryTransactionType.Sale, CreatedAt = new DateTime(2026, 10, 28, 8, 15, 0, DateTimeKind.Utc) },
        new() { Id = 2, ItemId = 10, Quantity = 2, UnitPrice = 59m, TransactionType = InventoryTransactionType.Sale, CreatedAt = new DateTime(2026, 10, 28, 10, 45, 0, DateTimeKind.Utc) },
        new() { Id = 3, ItemId = 20, Quantity = 4, UnitPrice = 115m, TransactionType = InventoryTransactionType.Sale, CreatedAt = new DateTime(2026, 10, 27, 20, 30, 0, DateTimeKind.Utc) },
        new() { Id = 4, ItemId = 24, Quantity = 1, UnitPrice = 297m, TransactionType = InventoryTransactionType.Sale, CreatedAt = new DateTime(2026, 10, 27, 19, 20, 0, DateTimeKind.Utc) },
        new() { Id = 5, ItemId = 36, Quantity = 1, UnitPrice = 47m, TransactionType = InventoryTransactionType.Sale, CreatedAt = new DateTime(2026, 10, 26, 22, 11, 0, DateTimeKind.Utc) },
        new() { Id = 6, ItemId = 27, Quantity = 2, UnitPrice = 572m, TransactionType = InventoryTransactionType.Sale, CreatedAt = new DateTime(2026, 10, 21, 21, 5, 0, 0, DateTimeKind.Utc) },
        new() { Id = 7, ItemId = 3, Quantity = 5, UnitPrice = 58m, TransactionType = InventoryTransactionType.Sale, CreatedAt = new DateTime(2026, 10, 19, 15, 40, 0, DateTimeKind.Utc) },
        new() { Id = 8, ItemId = 41, Quantity = 1, UnitPrice = 49m, TransactionType = InventoryTransactionType.Sale, CreatedAt = new DateTime(2026, 10, 13, 17, 32, 0, DateTimeKind.Utc) }
    ];
}