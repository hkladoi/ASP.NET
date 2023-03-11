using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mixi.Models;
namespace Mixi.Configurations
{
    public class UseConfigurationsr : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.UserID);
            builder.Property(c => c.FirstName).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(c => c.LastName).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(c => c.PhoneNumber).HasColumnType("nvarchar(10)").IsRequired();
            builder.Property(c => c.Address).HasColumnType("nvarchar(MAX)");
            builder.Property(c => c.Password).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(c => c.Email).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(c => c.Account).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();
            builder.HasOne(x => x.Roles).WithMany(y => y.Users).
            HasForeignKey(c => c.RoleID);
        }
    }
}
