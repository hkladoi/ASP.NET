using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_App.Models;

namespace Shopping_App.Configurations
{
    public class CartConfigurations : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("giohang");
            builder.HasKey(c => c.UserID);
            builder.Property(c => c.Description).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.HasOne(x => x.User).WithMany(y => y.Carts).HasForeignKey(x => x.UserID);
        }
    }
}
