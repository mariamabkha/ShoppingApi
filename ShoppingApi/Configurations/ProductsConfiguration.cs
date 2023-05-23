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
            builder.Property(p => p.Id).HasMaxLength(11).IsRequired();

   /*         builder.HasOne(p => p.UserAccount)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.UserId);

            builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);*/
        }
    }
}
