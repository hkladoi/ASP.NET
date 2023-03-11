using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mixi.Models;

namespace Mixi.Configurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(c => c.RoleID);
            builder.Property(c => c.RoleName).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();
        }
    }
}
