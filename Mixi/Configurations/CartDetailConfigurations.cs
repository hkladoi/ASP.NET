using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mixi.Models;

namespace Mixi.Configurations
{
    public class CartDetailConfigurations : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            throw new NotImplementedException();
        }
    }
}
