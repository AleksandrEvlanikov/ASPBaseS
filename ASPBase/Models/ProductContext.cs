

using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ASPBase.Models
{
    public class ProductContext : DbContext
    {
        //public DbSet<ProductStorege> productStoreges { get; set; }

        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=127.0.0.2;Port=3306;Database=ASPBase1Sem;User ID=root;Password=123456789Sasha;");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Product>(entity =>
        //    {
        //        entity.ToTable("Products");

        //        entity.HasKey(x => x.Id).HasName("ProductID");
        //        entity.HasIndex(x => x.Name).IsUnique();

        //        entity.Property(e => e.Name)
        //        .HasColumnName("ProductName")
        //        .HasMaxLength(255)
        //        .IsRequired();

        //        entity.Property(e => e.Description)
        //        .HasColumnName("Description")
        //        .HasMaxLength(255)
        //        .IsRequired();

        //        entity.Property(e => e.Price)
        //        .HasColumnName("Price")
        //        .IsRequired();

        //        entity.HasOne(x => x.Category)
        //        .WithMany(c => c.Products)
        //        .HasForeignKey(x => x.Id)
        //        .HasConstraintName("GroupToProduct");

        //    });
        //    modelBuilder.Entity<Category>(entity =>
        //    {
        //        entity.ToTable("ProductGroups");

        //        entity.HasKey(x => x.Id).HasName("CategoryId");
        //        entity.HasIndex(x => x.Name).IsUnique();

        //        entity.Property(e => e.Name)
        //        .HasColumnName("ProductName")
        //        .HasMaxLength(255)
        //        .IsRequired();
        //    });

        //    modelBuilder.Entity<Storage>(entity =>
        //    {

        //        entity.ToTable("Storage");

        //        entity.HasKey(x => x.Id).HasName("StoragID");


        //        entity.Property(e => e.Name)
        //        .HasColumnName("StorageName");

        //        entity.Property(e => e.ProductId)
        //        .HasColumnName("ProductID");

        //        entity.HasMany(x => x.Products)
        //        .WithMany(m => m.Storages)
        //        .UsingEntity(j => j.ToTable("StorageProduct"));
        //    });
        
    }
}
