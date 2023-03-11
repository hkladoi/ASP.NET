using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mixi.Models;

namespace Mixi.Configurations
{
    public class ImageConfigurations : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(c => c.ImageID);
            builder.Property(c => c.Name).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(c => c.LinkImage).HasColumnType("image").IsRequired();
            builder.HasOne(x => x.Product).WithMany(y => y.Images).
            HasForeignKey(c => c.ProductID);
        }
    }
}
