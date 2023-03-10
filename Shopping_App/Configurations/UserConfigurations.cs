using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_App.Models;

namespace Shopping_App.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("nguoidung");
            builder.HasKey(c => c.UserID);
            builder.Property(c => c.UserName).HasColumnType("nvarchar(1000)").IsRequired();
            builder.Property(c => c.Password).HasColumnType("nvarchar(1000)").IsRequired();
            builder.Property(c => c.status).HasColumnType("int").IsRequired();
            builder.HasOne(x => x.Roles).WithMany(y => y.Users).HasForeignKey(x => x.role);
        }
    }
}
