using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_App.Models;

namespace Shopping_App.Configurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("chucvu");
            builder.HasKey(c => c.RoleID);
            builder.Property(c => c.RoleName).HasColumnType("nvarchar(1000)").IsRequired();
            builder.Property(c => c.Description).HasColumnType("nvarchar(1000)").IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();
        }
    }
}
