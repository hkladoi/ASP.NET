using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mixi.Models;

namespace Mixi.Configurations
{
    public class PayConfigurations : IEntityTypeConfiguration<Pay>
    {
        public void Configure(EntityTypeBuilder<Pay> builder)
        {
            throw new NotImplementedException();
        }
    }
}
