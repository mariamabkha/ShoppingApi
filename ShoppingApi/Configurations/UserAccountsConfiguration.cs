using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Models;

namespace ShoppingApi.Configurations
{
    public class UserAccountsConfiguration : IEntityTypeConfiguration<UserAccounts>
    {
        public void Configure(EntityTypeBuilder<UserAccounts> builder)
        {
            builder.ToTable("UserAccounts");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasMaxLength(11);

           /* builder.HasOne(u => u.UserType)
            .WithMany(ut => ut.UserAccounts)
            .HasForeignKey(u => u.TypeId);*/

            builder.HasMany(u => u.Products)
                .WithOne(p => p.UserAccount)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Payments)
                .WithOne(p => p.UserAccount)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Carts)
                .WithOne(c => c.UserAccount)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Orders)
                .WithOne(o => o.UserAccount)
                .HasForeignKey(o => o.UserId).
                OnDelete(DeleteBehavior.Restrict); 

            builder.HasMany(u => u.Deliveries)
                .WithOne(d => d.UserAccount)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Transactions)
                .WithOne(t => t.UserAccount)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
