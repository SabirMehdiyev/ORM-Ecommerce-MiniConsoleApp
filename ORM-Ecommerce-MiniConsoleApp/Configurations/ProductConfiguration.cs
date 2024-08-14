namespace ORM_Ecommerce_MiniConsoleApp.Configurations;

public class ProductConfiguration:IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Price).IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.HasCheckConstraint("CK_Price", "Price > 0");

        builder.Property(p => p.Stock)
            .IsRequired();
        builder.HasCheckConstraint("CK_Stock", "Stock >= 0");

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.CreatedDate)
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow);
    }
}
