using System;
using System.Collections.Generic;
using System.Text;
using Designeo.Eshop.Core.Entities;
using Designeo.Eshop.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Designeo.Eshop.Infrastructure.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

        }
    }
}
