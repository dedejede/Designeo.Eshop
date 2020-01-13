using System;
using System.Collections.Generic;
using System.Text;
using Designeo.Eshop.Core.Entities;
using Designeo.Eshop.Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Designeo.Eshop.Infrastructure.Data
{
    public class EshopContext : DbContext
    {
        public EshopContext(DbContextOptions<EshopContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderItemConfiguration());
        }
    }
}
