using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_App.Models;

namespace Shopping_App.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("sanpham");
            builder.HasKey(c => c.ID);
            builder.Property(c => c.Name).HasColumnType("nvarchar(1000)").IsRequired();
            builder.Property(c => c.Price).HasColumnType("int").IsRequired();
            builder.Property(c => c.AvailableQuantity).HasColumnType("int").IsRequired();
            builder.Property(c => c.Supplier).HasColumnType("nvarchar(1000)").IsRequired();
            builder.Property(c => c.Description).HasColumnType("nvarchar(1000)").IsRequired();
            //builder.Property(c => c.Description).IsUnicode().HasMaxLength(1000).IsFixedLength();
            //2 cái des trên giống nhau
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();

        }
    }
}
