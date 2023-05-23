using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApi.Models;

namespace ShoppingApi.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");
            builder.Property(p => p.Id).HasMaxLength(11);
            builder.Property(p => p.ProductId).HasMaxLength(255);
            builder.Property(p => p.Quantity).HasMaxLength(255);
            builder.Property(p => p.Amount).HasMaxLength(11);
            builder.Property(p => p.UserId).HasMaxLength(11);

/*            builder.HasOne(p => p.UserAccount)
            .WithMany(u => u.Payments)
            .HasForeignKey(p => p.UserId);*/
        }
    }
}
