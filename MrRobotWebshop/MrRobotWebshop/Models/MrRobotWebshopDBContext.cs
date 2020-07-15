using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MrRobotWebshop.Models
{
    public partial class MrRobotWebshopDBContext : DbContext
    {
        public MrRobotWebshopDBContext()
        {
        }

        public MrRobotWebshopDBContext(DbContextOptions<MrRobotWebshopDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductInfo> ProductInfo { get; set; }
        public virtual DbSet<Receipt> Receipt { get; set; }
        public virtual DbSet<SubCategory> SubCategory { get; set; }
        public virtual DbSet<WebshopUser> WebshopUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=MrRobotWebshopDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SubCategoryId)
                    .HasConstraintName("FK__Product__SubCate__3F466844");
            });

            modelBuilder.Entity<ProductInfo>(entity =>
            {
                entity.Property(e => e.ProductInfoId)
                    .HasColumnName("ProductInfoID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ReceiptId).HasColumnName("ReceiptID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInfo)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ProductIn__Produ__4222D4EF");

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.ProductInfo)
                    .HasForeignKey(d => d.ReceiptId)
                    .HasConstraintName("FK__ProductIn__Recei__4316F928");
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.Property(e => e.ReceiptId)
                    .HasColumnName("ReceiptID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FinalPrice)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.Property(e => e.SubCategoryId)
                    .HasColumnName("SubCategoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.SubCategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__SubCatego__Categ__38996AB5");
            });

            modelBuilder.Entity<WebshopUser>(entity =>
            {
                entity.Property(e => e.WebshopUserId)
                    .HasColumnName("WebshopUserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
