using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Models;

namespace ShoppingApi.Configurations
{
    public class UserTypesConfiguration : IEntityTypeConfiguration<UserTypes>
    {
        public void Configure(EntityTypeBuilder<UserTypes> builder)
        {
            builder.ToTable("UserTypes");
            builder.HasKey(ut => ut.Id);
            builder.Property(u => u.Id).HasMaxLength(11);

            builder.HasMany(ut => ut.UserAccounts)
            .WithOne(u => u.UserType)
            .HasForeignKey(u => u.TypeId);
        }
    }
}
