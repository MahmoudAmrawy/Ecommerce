using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired();

            builder.Property(n => n.Name)
                .HasColumnType("varchar")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(n => n.Description)
                .HasColumnType("varchar")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.PictureUrl)
                .HasColumnType("varchar")
                .HasMaxLength(250)
                .IsRequired();

            builder.HasOne(b => b.ProductBrand)
                .WithMany()
                .HasForeignKey(b => b.ProductBrandId);

            builder.HasOne(t => t.ProductType)
                .WithMany()
                .HasForeignKey(t => t.ProductTypeId);
        }
    }
}
