namespace ORM_Ecommerce_MiniConsoleApp.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(p => p.Amount)
            .HasColumnType("decimal(10,2)").IsRequired();

        builder.Property(p => p.PaymentDate)
            .IsRequired();
    }
}