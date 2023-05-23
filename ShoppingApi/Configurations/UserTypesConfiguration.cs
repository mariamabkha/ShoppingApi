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
            builder.Property(u => u.Id).HasMaxLength(11);
        }
    }
}
