using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    internal class ProductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            //builder.HasData(new ProductFeature { Id = 1, ProductId = 2, Color = "Kırmızı", Height = 20, Width = 20 },
            //                new ProductFeature { Id = 2, ProductId = 1, Color = "Siyah", Height = 30, Width = 30 });
        }
    }
}
