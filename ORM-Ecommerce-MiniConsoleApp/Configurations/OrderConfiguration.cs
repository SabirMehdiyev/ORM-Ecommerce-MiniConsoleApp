namespace ORM_Ecommerce_MiniConsoleApp.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.OrderDate).IsRequired();

        builder.Property(o => o.TotalAmount).HasColumnType("decimal(10,2)").IsRequired();

        builder.Property(o => o.Status).IsRequired();

        builder.HasCheckConstraint("CK_Order_Status_Range", "Status > 0 AND Status < 4");
    }
}
