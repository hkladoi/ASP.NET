using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_App.Models;
namespace Shopping_App.Configurations
{
    public class BillConfigurations : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("HoaDon"); //đặt tên cho bảng
            builder.HasKey(x => x.ID);//khoá chính
            //set các thuộc tính
            builder.Property(x => x.CreateDate).HasColumnType("Datetime").IsRequired();
            builder.Property(x => x.Status).HasColumnType("nvarchar(1000)").IsRequired();
            builder.HasOne(c => c.User).WithMany(y => y.bill).HasForeignKey(c=>c.UserID);
        }
    }
}
