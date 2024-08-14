using Microsoft.EntityFrameworkCore;
using ORM_Ecommerce_MiniConsoleApp.Configurations;
using ORM_Ecommerce_MiniConsoleApp.Models;

namespace ORM_Ecommerce_MiniConsoleApp.Contexts;

public class AppDbContext:DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Payment> Payments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDetailConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=DESKTOP-OP8UPED\\TESTSERVER2;Database=Ecommerce-ConsoleApp;Trusted_Connection=True";
        optionsBuilder.UseSqlServer(connectionString);
    }
}
