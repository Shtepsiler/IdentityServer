using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DAL.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // builder.Property<int>(p => p.Id).UseIdentityColumn();

            builder.Property(p => p.UserName).HasMaxLength(256);

            //builder.HasKey(p => p.Id);
        }
    }
}
