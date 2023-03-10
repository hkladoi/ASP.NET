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
            builder.HasOne(x => x.Cart).WithMany(y => y.cartdetail).HasForeignKey(x => x.UserID).HasConstraintName("FK_CART");
            builder.HasOne(c => c.Product).WithMany(d => d.CartDetails).HasForeignKey(c => c.IDSP).HasConstraintName("FK_Product");
        }
    }
}
