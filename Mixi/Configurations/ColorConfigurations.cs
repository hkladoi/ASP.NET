using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mixi.Models;

namespace Mixi.Configurations
{
    public class ColorConfigurations : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(c => c.ColorID);
            builder.Property(c => c.Name).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();
        }
    }
}
