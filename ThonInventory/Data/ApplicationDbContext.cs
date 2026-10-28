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
            .HasIndex(entry => entry.SoldAt);

        SeedSheetData(modelBuilder);
    }

    private static void SeedSheetData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "BRUS", DisplayOrder = 1 },
            new Category { Id = 2, Name = "SNACKS", DisplayOrder = 2 },
            new Category { Id = 3, Name = "ALKOHOL", DisplayOrder = 3 },
            new Category { Id = 4, Name = "KIOSK", DisplayOrder = 4 });

        modelBuilder.Entity<Item>().HasData(CreateSeedItems());
        modelBuilder.Entity<SalesLogEntry>().HasData(CreateSeedSalesEntries());
    }

    private static Item[] CreateSeedItems() =>
    [
        new() { Id = 1, CategoryId = 1, Name = "Coca-Cola", DisplayOrder = 1, Quantity = 48, Price = 58m },
        new() { Id = 2, CategoryId = 1, Name = "Coca-Cola Zero", DisplayOrder = 2, Quantity = 44, Price = 58m },
        new() { Id = 3, CategoryId = 1, Name = "Fanta", DisplayOrder = 3, Quantity = 26, Price = 58m, IsOrdered = true, OrderedAtDate = new DateTime(2026, 10, 27, 9, 0, 0, DateTimeKind.Utc), ExpectedDeliveryDate = new DateTime(2026, 10, 30, 11, 0, 0, DateTimeKind.Utc) },
        new() { Id = 4, CategoryId = 1, Name = "Sprite", DisplayOrder = 4, Quantity = 31, Price = 58m },
        new() { Id = 5, CategoryId = 1, Name = "Eplemost", DisplayOrder = 5, Quantity = 14, Price = 89m, IsOrdered = true, OrderedAtDate = new DateTime(2026, 10, 27, 9, 5, 0, DateTimeKind.Utc), ExpectedDeliveryDate = new DateTime(2026, 10, 31, 12, 0, 0, DateTimeKind.Utc) },
        new() { Id = 6, CategoryId = 1, Name = "Mineralvann", DisplayOrder = 6, Quantity = 23, Price = 58m },
        new() { Id = 7, CategoryId = 1, Name = "Mineralvann med kullsyre", DisplayOrder = 7, Quantity = 19, Price = 58m },
        new() { Id = 8, CategoryId = 1, Name = "Red Bull", DisplayOrder = 8, Quantity = 12, Price = 39m, IsMissing = true, MissingSinceDate = new DateTime(2026, 10, 26, 8, 0, 0, DateTimeKind.Utc), Notes = "Hylle tom i minibarrom" },
        new() { Id = 9, CategoryId = 1, Name = "Monster", DisplayOrder = 9, Quantity = 10, Price = 68m, IsMissing = true, MissingSinceDate = new DateTime(2026, 10, 26, 8, 10, 0, DateTimeKind.Utc) },
        new() { Id = 10, CategoryId = 2, Name = "Stratos", DisplayOrder = 1, Quantity = 51, Price = 59m },
        new() { Id = 11, CategoryId = 2, Name = "Melkesjokolade", DisplayOrder = 2, Quantity = 39, Price = 59m },
        new() { Id = 12, CategoryId = 2, Name = "Sørlandschips", DisplayOrder = 3, Quantity = 22, Price = 59m, IsOrdered = true, OrderedAtDate = new DateTime(2026, 10, 25, 13, 0, 0, DateTimeKind.Utc), ExpectedDeliveryDate = new DateTime(2026, 10, 29, 10, 0, 0, DateTimeKind.Utc) },
        new() { Id = 13, CategoryId = 2, Name = "Sørlandschips (små poser)", DisplayOrder = 4, Quantity = 28, Price = 59m },
        new() { Id = 14, CategoryId = 2, Name = "Chillinøtter", DisplayOrder = 5, Quantity = 18, Price = 42m },
        new() { Id = 15, CategoryId = 2, Name = "Peanøtter", DisplayOrder = 6, Quantity = 16, Price = 42m },
        new() { Id = 16, CategoryId = 2, Name = "Polly Nøtter", DisplayOrder = 7, Quantity = 27, Price = 42m },
        new() { Id = 17, CategoryId = 2, Name = "Extra tyggis", DisplayOrder = 8, Quantity = 60, Price = 32m },
        new() { Id = 18, CategoryId = 2, Name = "Små sulten", DisplayOrder = 9, Quantity = 18, Price = 38m },
        new() { Id = 19, CategoryId = 2, Name = "Is", DisplayOrder = 10, Quantity = 40, Price = 45m },
        new() { Id = 20, CategoryId = 3, Name = "Carlsberg", DisplayOrder = 1, Quantity = 47, Price = 115m },
        new() { Id = 21, CategoryId = 3, Name = "Alkoholfri øl", DisplayOrder = 2, Quantity = 36, Price = 79m },
        new() { Id = 22, CategoryId = 3, Name = "Gin-Tonic i boks", DisplayOrder = 3, Quantity = 25, Price = 115m },
        new() { Id = 23, CategoryId = 3, Name = "Bulmers Cider", DisplayOrder = 4, Quantity = 17, Price = 159m },
        new() { Id = 24, CategoryId = 3, Name = "Prosecco 200ml (små flaske)", DisplayOrder = 5, Quantity = 34, Price = 297m },
        new() { Id = 25, CategoryId = 3, Name = "Prosecco 750ml", DisplayOrder = 6, Quantity = 12, Price = 624m, IsOrdered = true, OrderedAtDate = new DateTime(2026, 10, 24, 14, 30, 0, DateTimeKind.Utc), ExpectedDeliveryDate = new DateTime(2026, 10, 30, 14, 0, 0, DateTimeKind.Utc) },
        new() { Id = 26, CategoryId = 3, Name = "Rødvin 37.5cl (små flaske)", DisplayOrder = 7, Quantity = 20, Price = 315m },
        new() { Id = 27, CategoryId = 3, Name = "Rødvin 750ml", DisplayOrder = 8, Quantity = 15, Price = 572m },
        new() { Id = 28, CategoryId = 3, Name = "Hvitvin 375ml (små flaske)", DisplayOrder = 9, Quantity = 16, Price = 315m },
        new() { Id = 29, CategoryId = 3, Name = "Hvitvin 750ml", DisplayOrder = 10, Quantity = 11, Price = 572m },
        new() { Id = 30, CategoryId = 3, Name = "Jameson", DisplayOrder = 11, Quantity = 8, Price = 126m, IsOrdered = true, OrderedAtDate = new DateTime(2026, 10, 22, 15, 0, 0, DateTimeKind.Utc), ExpectedDeliveryDate = new DateTime(2026, 10, 31, 12, 0, 0, DateTimeKind.Utc) },
        new() { Id = 31, CategoryId = 3, Name = "Rum (Captain Morgan)", DisplayOrder = 12, Quantity = 9, Price = 126m },
        new() { Id = 32, CategoryId = 3, Name = "Cognac", DisplayOrder = 13, Quantity = 4, Price = 126m },
        new() { Id = 33, CategoryId = 3, Name = "Gin", DisplayOrder = 14, Quantity = 7, Price = 126m },
        new() { Id = 34, CategoryId = 3, Name = "Pink Gin", DisplayOrder = 15, Quantity = 6, Price = 126m },
        new() { Id = 35, CategoryId = 3, Name = "Tonic Water", DisplayOrder = 16, Quantity = 33, Price = 39m },
        new() { Id = 36, CategoryId = 4, Name = "Tannbørste", DisplayOrder = 1, Quantity = 14, Price = 47m },
        new() { Id = 37, CategoryId = 4, Name = "Tannkrem", DisplayOrder = 2, Quantity = 18, Price = 42m },
        new() { Id = 38, CategoryId = 4, Name = "OB rosa", DisplayOrder = 3, Quantity = 22, Price = 37m },
        new() { Id = 39, CategoryId = 4, Name = "OB blå", DisplayOrder = 4, Quantity = 20, Price = 37m },
        new() { Id = 40, CategoryId = 4, Name = "Bind", DisplayOrder = 5, Quantity = 13, Price = 42m },
        new() { Id = 41, CategoryId = 4, Name = "Kortstokk", DisplayOrder = 6, Quantity = 9, Price = 49m },
        new() { Id = 42, CategoryId = 4, Name = "Yatzy", DisplayOrder = 7, Quantity = 7, Price = 79m }
    ];

    private static SalesLogEntry[] CreateSeedSalesEntries() =>
    [
        new() { Id = 1, ItemId = 1, QuantitySold = 3, UnitPrice = 58m, SoldAt = new DateTime(2026, 10, 28, 8, 15, 0, DateTimeKind.Utc) },
        new() { Id = 2, ItemId = 10, QuantitySold = 2, UnitPrice = 59m, SoldAt = new DateTime(2026, 10, 28, 10, 45, 0, DateTimeKind.Utc) },
        new() { Id = 3, ItemId = 20, QuantitySold = 4, UnitPrice = 115m, SoldAt = new DateTime(2026, 10, 27, 20, 30, 0, DateTimeKind.Utc) },
        new() { Id = 4, ItemId = 24, QuantitySold = 1, UnitPrice = 297m, SoldAt = new DateTime(2026, 10, 27, 19, 20, 0, DateTimeKind.Utc) },
        new() { Id = 5, ItemId = 36, QuantitySold = 1, UnitPrice = 47m, SoldAt = new DateTime(2026, 10, 26, 22, 11, 0, DateTimeKind.Utc) },
        new() { Id = 6, ItemId = 27, QuantitySold = 2, UnitPrice = 572m, SoldAt = new DateTime(2026, 10, 21, 21, 5, 0, DateTimeKind.Utc) },
        new() { Id = 7, ItemId = 3, QuantitySold = 5, UnitPrice = 58m, SoldAt = new DateTime(2026, 10, 19, 15, 40, 0, DateTimeKind.Utc) },
        new() { Id = 8, ItemId = 41, QuantitySold = 1, UnitPrice = 49m, SoldAt = new DateTime(2026, 10, 13, 17, 32, 0, DateTimeKind.Utc) }
    ];
}
