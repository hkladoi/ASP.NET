using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mixi.Models;

namespace Mixi.Configurations
{
    public class BillDetailConfigurations : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(c => c.Quantity).HasColumnType("int").IsRequired();
            builder.Property(c => c.Price).HasColumnType("decimal").IsRequired();
            builder.HasOne(x => x.bill).WithMany(y => y.BillDetails).HasForeignKey(c => c.BillID);
            builder.HasOne(x => x.product).WithMany(y => y.BillDetails).HasForeignKey(c => c.ProductID);
        }
    }
}
