using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Models;

namespace ShoppingApi.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasMaxLength(11);

        /*    builder.HasOne(t => t.UserAccount)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId);*/
        }
    }
}
