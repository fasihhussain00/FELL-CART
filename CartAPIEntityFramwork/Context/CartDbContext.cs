using System;
using CartAPIEntityFramwork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace CartAPIEntityFramwork.Context
{
    public partial class CartDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public CartDbContext()
        {
        }

        public CartDbContext(DbContextOptions<CartDbContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartDetail> CartDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultDBConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("carts");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Customerid).HasColumnName("customerid");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("FK__carts__customeri__68487DD7");
            });

            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.ToTable("cartDetails");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Cartid).HasColumnName("cartid");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Quantity)
                    .HasColumnType("decimal(10, 3)")
                    .HasColumnName("quantity");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.Cartid)
                    .HasConstraintName("FK__cartDetai__carti__6D0D32F4");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("FK__cartDetai__produ__6E01572D");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Category1)
                    .HasMaxLength(100)
                    .HasColumnName("category");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Metadata)
                    .HasMaxLength(500)
                    .HasColumnName("metadata");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 3)")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity)
                    .HasColumnType("decimal(10, 3)")
                    .HasColumnName("quantity");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("FK__products__catego__6383C8BA");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiryDate");

                entity.Property(e => e.IsRevoked).HasColumnName("isRevoked");

                entity.Property(e => e.IsUsed).HasColumnName("isUsed");

                entity.Property(e => e.JwtId)
                    .IsRequired()
                    .HasColumnName("jwtId");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshToken_customers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
