using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Models;

namespace ShoppingApi.Configurations
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasMaxLength(11);

         /*   builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);*/

            builder.HasMany(p => p.Payments)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);

            builder.HasMany(p => p.Carts)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId);

            /*         builder.HasOne(p => p.UserAccount)
                     .WithMany(u => u.Products)
                     .HasForeignKey(p => p.UserId);

                     builder.HasOne(p => p.Category)
                     .WithMany(c => c.Products)
                     .HasForeignKey(p => p.CategoryId);*/
        }
    }
}
