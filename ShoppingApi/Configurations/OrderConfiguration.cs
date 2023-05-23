using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApi.Models;

namespace ShoppingApi.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.Property(o => o.Id).HasMaxLength(11);
            builder.Property(o => o.UserId).HasMaxLength(11);
            builder.Property(o => o.CartId).HasMaxLength(11);
            //builder.Property(o => o.IsComplete).HasMaxLength(11);

   /*         builder.HasOne(o => o.UserAccount)
           .WithMany(u => u.Orders)
           .HasForeignKey(o => o.UserId);*/
        }
    }
}
