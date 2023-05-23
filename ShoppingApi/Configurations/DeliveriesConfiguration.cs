using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApi.Models;

namespace ShoppingApi.Configurations
{
    public class DeliveriesConfiguration : IEntityTypeConfiguration<Deliveries>
    {
        public void Configure(EntityTypeBuilder<Deliveries> builder)
        {
            builder.ToTable("Deliveries");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id).HasMaxLength(11);
            builder.Property(d => d.UserId).HasMaxLength(11);

           /* builder.HasOne(d => d.UserAccount)
            .WithMany(u => u.Deliveries)
            .HasForeignKey(d => d.UserId);*/

        }
    }
}
