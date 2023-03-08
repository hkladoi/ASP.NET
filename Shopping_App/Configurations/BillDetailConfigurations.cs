using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_App.Models;

namespace Shopping_App.Configurations
{
    public class BillDetailConfigurations : IEntityTypeConfiguration<BillDetails>
    {
        public void Configure(EntityTypeBuilder<BillDetails> builder)
        {
            builder.HasKey(p => p.ID);
            //set thuộc tính
            builder.Property(p => p.Price).HasColumnType("int");
            builder.Property(p => p.Quantity).HasColumnType("int");
            //set khoá ngoại
            builder.HasOne(x => x.bill).WithMany(y => y.Details).HasForeignKey(x => x.IDHD);
            builder.HasOne(d => d.product).WithMany(c => c.BillDetails).HasForeignKey(d => d.IDSP);
        }
    }
}
