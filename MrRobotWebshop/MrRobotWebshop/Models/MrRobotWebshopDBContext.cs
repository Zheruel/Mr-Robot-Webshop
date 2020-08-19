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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=MrRobotWebshopDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SubCategoryId)
                    .HasConstraintName("FK__Product__SubCate__7EF6D905");
            });

            modelBuilder.Entity<ProductInfo>(entity =>
            {
                entity.Property(e => e.ProductInfoId).HasColumnName("ProductInfoID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ReceiptId).HasColumnName("ReceiptID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInfo)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ProductIn__Produ__01D345B0");

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.ProductInfo)
                    .HasForeignKey(d => d.ReceiptId)
                    .HasConstraintName("FK__ProductIn__Recei__02C769E9");
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.Property(e => e.ReceiptId).HasColumnName("ReceiptID");

                entity.Property(e => e.FinalPrice)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WebshopUserId).HasColumnName("WebshopUserID");

                entity.HasOne(d => d.WebshopUser)
                    .WithMany(p => p.Receipt)
                    .HasForeignKey(d => d.WebshopUserId)
                    .HasConstraintName("FK__Receipt__Webshop__7C1A6C5A");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.SubCategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__SubCatego__Categ__7755B73D");
            });

            modelBuilder.Entity<WebshopUser>(entity =>
            {
                entity.Property(e => e.WebshopUserId).HasColumnName("WebshopUserID");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
