using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mixi.Models;

namespace Mixi.Configurations
{
    public class SizeConfigurations : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.HasKey(c => c.SizeID);
            builder.Property(c => c.Name).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").IsRequired();

        }
    }
}
