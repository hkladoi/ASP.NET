using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mixi.Models;

namespace Mixi.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.ProductID);
            builder.Property(c => c.Name).HasColumnType("nvarchar(1000)").IsRequired();
            builder.Property(c => c.ProductCode).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.Property(c => c.Price).HasColumnType("decimal").IsRequired();
            builder.Property(c => c.SalePrice).HasColumnType("decimal").IsRequired();
            builder.Property(c => c.AvailableQuantity).HasColumnType("int").IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();
            builder.Property(c => c.Supplier).HasColumnType("nvarchar(1000)").IsRequired();
            builder.Property(c => c.Description).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.HasOne(x => x.Color).WithMany(y => y.Products).HasForeignKey(c => c.ColorID);
            builder.HasOne(x => x.Size).WithMany(y => y.Products).HasForeignKey(c => c.SizeID);
            builder.HasOne(x => x.Category).WithMany(y => y.Products).HasForeignKey(c => c.CategoryID);
        }
    }
}
