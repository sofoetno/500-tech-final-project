using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject;

public class EcommerceDbContext : DbContext
{
    //public EcommerceDBContext(DbContextOptions<EcommerceDBContext> options) : base(options)
    //{
    //}
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=localhost;User Id=sa;Password=123qwe!@#QWE;Database=EcommerceDB;TrustServerCertificate=True;Encrypt=false;MultiSubnetFailover=True");
    }
    
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserType>()
            .HasMany(e => e.UserAccounts)
            .WithOne(e => e.UserType)
            .HasForeignKey(e => e.UserTypeId)
            .IsRequired();

        modelBuilder.Entity<Cart>()
            .HasOne(e => e.UserAccount)
            .WithMany()
            .HasForeignKey(e => e.UserAccountId)
            .IsRequired();

        modelBuilder.Entity<Category>()
            .HasMany(e => e.Products)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId)
            .IsRequired();

        modelBuilder.Entity<Cart>()
            .HasMany(e => e.Items)
            .WithOne(e => e.Cart)
            .HasForeignKey(e => e.CartId);

        modelBuilder.Entity<CartItem>()
            .HasOne(e => e.Product)
            .WithMany()
            .HasForeignKey(e => e.ProductId)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .HasOne(e => e.Cart)
            .WithOne(e => e.Order)
            .HasForeignKey<Order>(e => e.CartId)
            .IsRequired();

        modelBuilder.Entity<Payment>()
            .HasOne(e => e.UserAccount)
            .WithMany(e => e.Payments)
            .HasForeignKey(e => e.UserAccountId)
            .IsRequired();
        
        modelBuilder.Entity<Payment>()
            .HasOne(e => e.Order)
            .WithOne()
            .HasForeignKey<Payment>(e => e.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Delivery>()
            .HasOne(e => e.Order)
            .WithOne(e => e.Delivery)
            .HasForeignKey<Delivery>(e => e.OrderId)
            .IsRequired();
        
        modelBuilder.Entity<Transaction>()
            .HasOne(e => e.UserAccount)
            .WithMany()
            .HasForeignKey(e => e.UserAccountId)
            .IsRequired();
    }
}