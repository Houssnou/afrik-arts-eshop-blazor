using System.Collections.Generic;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductOriginConfiguration : IEntityTypeConfiguration<ProductOrigin>
    {
        public void Configure(EntityTypeBuilder<ProductOrigin> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

            //builder.HasData(
            //    new ProductOrigin
            //    {

            //    },

            //    )
        }
    }
}