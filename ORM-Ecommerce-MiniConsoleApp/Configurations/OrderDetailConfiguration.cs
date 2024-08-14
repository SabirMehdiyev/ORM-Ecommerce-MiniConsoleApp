namespace ORM_Ecommerce_MiniConsoleApp.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.Property(od => od.Quantity).IsRequired();
        builder.Property(od => od.PricePerItem)
            .HasColumnType("decimal(10,2)")
            .IsRequired()
            .IsRequired();

        builder.HasCheckConstraint("CK_OrderDetail_PricePerItem", "PricePerItem > 0");

    }
}