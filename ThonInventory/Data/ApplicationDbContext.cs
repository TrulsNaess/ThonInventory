using Microsoft.EntityFrameworkCore;
using ThonInventory.Models;

namespace ThonInventory.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Category> Categories => Set<Category>();

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

        // Seed data mirrors the current manual sheet so the frontend can start immediately.
        SeedSheetData(modelBuilder);
    }

    private static void SeedSheetData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "BRUS", DisplayOrder = 1 },
            new Category { Id = 2, Name = "SNACKS", DisplayOrder = 2 },
            new Category { Id = 3, Name = "ALKOHOL", DisplayOrder = 3 },
            new Category { Id = 4, Name = "KIOSK", DisplayOrder = 4 });

        modelBuilder.Entity<Item>().HasData(
            new Item { Id = 1, CategoryId = 1, Name = "Coca-Cola", DisplayOrder = 1 },
            new Item { Id = 2, CategoryId = 1, Name = "Coca-Cola Zero", DisplayOrder = 2 },
            new Item { Id = 3, CategoryId = 1, Name = "Fanta", DisplayOrder = 3 },
            new Item { Id = 4, CategoryId = 1, Name = "Sprite", DisplayOrder = 4 },
            new Item { Id = 5, CategoryId = 1, Name = "Eplemost", DisplayOrder = 5 },
            new Item { Id = 6, CategoryId = 1, Name = "Mineralvann", DisplayOrder = 6 },
            new Item { Id = 7, CategoryId = 1, Name = "Mineralvann med kullsyre", DisplayOrder = 7 },
            new Item { Id = 8, CategoryId = 1, Name = "Red Bull", DisplayOrder = 8 },
            new Item { Id = 9, CategoryId = 1, Name = "Monster", DisplayOrder = 9 },
            new Item { Id = 10, CategoryId = 2, Name = "Stratos", DisplayOrder = 1 },
            new Item { Id = 11, CategoryId = 2, Name = "Melkesjokolade", DisplayOrder = 2 },
            new Item { Id = 12, CategoryId = 2, Name = "Sørlandschips", DisplayOrder = 3 },
            new Item { Id = 13, CategoryId = 2, Name = "Sørlandschips (små poser)", DisplayOrder = 4 },
            new Item { Id = 14, CategoryId = 2, Name = "Chillinøtter", DisplayOrder = 5 },
            new Item { Id = 15, CategoryId = 2, Name = "Peanøtter", DisplayOrder = 6 },
            new Item { Id = 16, CategoryId = 2, Name = "Polly Nøtter", DisplayOrder = 7 },
            new Item { Id = 17, CategoryId = 2, Name = "Extra tyggis", DisplayOrder = 8 },
            new Item { Id = 18, CategoryId = 2, Name = "Små sulten", DisplayOrder = 9 },
            new Item { Id = 19, CategoryId = 2, Name = "Is", DisplayOrder = 10 },
            new Item { Id = 20, CategoryId = 3, Name = "Carlsberg", DisplayOrder = 1 },
            new Item { Id = 21, CategoryId = 3, Name = "Alkoholfri øl", DisplayOrder = 2 },
            new Item { Id = 22, CategoryId = 3, Name = "Gin-Tonic i boks", DisplayOrder = 3 },
            new Item { Id = 23, CategoryId = 3, Name = "Bulmers Cider", DisplayOrder = 4 },
            new Item { Id = 24, CategoryId = 3, Name = "Prosecco 200ml (små flaske)", DisplayOrder = 5 },
            new Item { Id = 25, CategoryId = 3, Name = "Prosecco 750ml", DisplayOrder = 6 },
            new Item { Id = 26, CategoryId = 3, Name = "Rødvin 37.5cl (små flaske)", DisplayOrder = 7 },
            new Item { Id = 27, CategoryId = 3, Name = "Rødvin 750ml", DisplayOrder = 8 },
            new Item { Id = 28, CategoryId = 3, Name = "Hvitvin 375ml (små flaske)", DisplayOrder = 9 },
            new Item { Id = 29, CategoryId = 3, Name = "Hvitvin 750ml", DisplayOrder = 10 },
            new Item { Id = 30, CategoryId = 3, Name = "Jameson", DisplayOrder = 11 },
            new Item { Id = 31, CategoryId = 3, Name = "Rum (Captain Morgan)", DisplayOrder = 12 },
            new Item { Id = 32, CategoryId = 3, Name = "Cognac", DisplayOrder = 13 },
            new Item { Id = 33, CategoryId = 3, Name = "Gin", DisplayOrder = 14 },
            new Item { Id = 34, CategoryId = 3, Name = "Pink Gin", DisplayOrder = 15 },
            new Item { Id = 35, CategoryId = 3, Name = "Tonic Water", DisplayOrder = 16 },
            new Item { Id = 36, CategoryId = 4, Name = "Tannbørste", DisplayOrder = 1 },
            new Item { Id = 37, CategoryId = 4, Name = "Tannkrem", DisplayOrder = 2 },
            new Item { Id = 38, CategoryId = 4, Name = "OB rosa", DisplayOrder = 3 },
            new Item { Id = 39, CategoryId = 4, Name = "OB blå", DisplayOrder = 4 },
            new Item { Id = 40, CategoryId = 4, Name = "Bind", DisplayOrder = 5 },
            new Item { Id = 41, CategoryId = 4, Name = "Kortstokk", DisplayOrder = 6 },
            new Item { Id = 42, CategoryId = 4, Name = "Yatzy", DisplayOrder = 7 });
    }
}
