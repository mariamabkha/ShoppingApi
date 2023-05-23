using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApi.Models;

namespace ShoppingApi.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasMaxLength(11);
            builder.Property(o => o.UserId).HasMaxLength(11);
            builder.Property(o => o.CartId).HasMaxLength(11);

            /*builder.HasOne(o => o.UserAccount)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

            builder.HasOne(o => o.Cart)
                .WithOne(c => c.Order)
                .HasForeignKey<Order>(o => o.CartId);*/
        }
    }
}
