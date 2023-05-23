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
            builder.Property(c => c.Id).HasMaxLength(11);
            builder.Property(c => c.ProductId).HasMaxLength(11);
            builder.Property(c => c.UserId).HasMaxLength(11);
            builder.Property(c => c.Count).HasMaxLength(11);
            builder.Property(c => c.IsFilled).HasMaxLength(11);



        }
    }
}
