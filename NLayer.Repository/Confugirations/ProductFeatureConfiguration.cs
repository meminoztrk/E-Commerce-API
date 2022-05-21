using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Confugirations
{
    internal class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.FePrice).IsRequired().HasColumnType("decimal(18,2)");


            builder.ToTable("ProductFeatures");

            builder.HasOne(x => x.Product).WithMany(x => x.ProductFeatures).HasForeignKey(x => x.ProductId);
        }
    }
}
