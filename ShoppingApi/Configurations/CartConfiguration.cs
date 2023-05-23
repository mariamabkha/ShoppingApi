using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApi.Models;

namespace ShoppingApi.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasMaxLength(11);
            builder.Property(c => c.ProductId).HasMaxLength(11);
            builder.Property(c => c.UserId).HasMaxLength(11);
            builder.Property(c => c.Count).HasMaxLength(11);

           /* builder.HasOne(c => c.Product)
            .WithMany(p => p.Carts)
            .HasForeignKey(c => c.ProductId);

            builder.HasOne(c => c.UserAccount)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId);

            builder.HasOne(c => c.Order)
                .WithOne(o => o.Cart)
                .HasForeignKey<Order>(o => o.CartId);*/

        }
    }
}
