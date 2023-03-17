using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mixi.Models;

namespace Mixi.Configurations
{
    public class BillConfigurations : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(c => c.BillID);
            builder.Property(c => c.Name).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(c => c.BillCode).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.Property(c => c.Phone).HasColumnType("nvarchar(10)").IsRequired();
            builder.Property(c => c.Address).HasColumnType("nvarchar(1000)").IsRequired();
            builder.Property(c => c.Description).HasColumnType("nvarchar(MAX)").IsRequired(false);
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();
            builder.Property(c => c.CreateDate).HasColumnType("Datetime").IsRequired();
            builder.HasOne(x => x.User).WithMany(y => y.bill).HasForeignKey(c => c.UserID);
        }
    }
}
