using System;
using System.Collections.Generic;
using System.Text;
using Designeo.Eshop.Core.Entities;
using Designeo.Eshop.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Designeo.Eshop.Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            var stateEnumConverter = new EnumToNumberConverter<OrderState, int>();
            builder.Property(p => p.State)
                .HasConversion(stateEnumConverter);

            builder.Ignore(p => p.IsReadOnly);
            
            var navigation = builder.Metadata.FindNavigation(nameof(Order.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
