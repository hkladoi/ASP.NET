using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_App.Models;

namespace Shopping_App.Configurations
{
    public class CartDetailConfigurations : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).HasColumnType("int").IsRequired();
            builder.HasOne(x => x.Cart).WithMany(y => y.cartdetail).HasForeignKey(x => x.UserID);
            builder.HasOne(c => c.Product).WithMany(d => d.CartDetails).HasForeignKey(c => c.IDSP);
        }
    }
}
