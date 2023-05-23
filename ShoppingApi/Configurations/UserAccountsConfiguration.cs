using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Models;

namespace ShoppingApi.Configurations
{
    public class UserAccountsConfiguration : IEntityTypeConfiguration<UserAccounts>
    {
        public void Configure(EntityTypeBuilder<UserAccounts> builder)
        {
            builder.ToTable("UserAcounts");
            builder.Property(u => u.Id).HasMaxLength(11);

            builder.HasMany(u => u.Products)
            .WithOne(p => p.UserAccount)
            .HasForeignKey(p => p.UserId);

            builder.HasMany(u => u.Payments)
           .WithOne(p => p.UserAccount)
           .HasForeignKey(p => p.UserId);

            builder.HasMany(u => u.Orders)
            .WithOne(o => o.UserAccount)
            .HasForeignKey(o => o.UserId);

            builder.HasMany(u => u.Deliveries)
           .WithOne(d => d.UserAccount)
           .HasForeignKey(d => d.UserId);
        }
    }
}
