using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mixi.Models;

namespace Mixi.Configurations
{
    public class CartDetailConfigurations : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.HasKey(x => x.CartID);
            builder.Property(c => c.Quantity).HasColumnType("int").IsRequired();
            builder.HasOne(x => x.Cart).WithMany(y => y.cartdetail).HasForeignKey(x => x.UserID);
            builder.HasOne(c => c.Product).WithMany(d => d.CartDetails).HasForeignKey(c => c.ProductID);
        }
    }
}
