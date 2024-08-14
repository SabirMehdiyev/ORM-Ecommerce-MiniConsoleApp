namespace ORM_Ecommerce_MiniConsoleApp.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).HasMaxLength(100);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Password).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Address).HasMaxLength(250);
    }
}
