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
            builder.Property(c => c.Name).HasColumnType("nvarchar(100)");
            builder.Property(c => c.LinkImage).HasColumnType("image").IsRequired();
            builder.Property(c => c.LinkImage1).HasColumnType("image").IsRequired(false);
            builder.Property(c => c.LinkImage2).HasColumnType("image").IsRequired(false);
            builder.Property(c => c.LinkImage3).HasColumnType("image").IsRequired(false);
            builder.Property(c => c.LinkImage4).HasColumnType("image").IsRequired(false);
        }
    }
}
