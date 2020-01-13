using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Designeo.Eshop.Core.Enums;

namespace Designeo.Eshop.Core.Entities
{
    public class OrderItem
    {
        public long OrderId { get; set; }
        public long Id { get; set; }
        public decimal Price { get; set; }

        public string Name { get; set; }
    }
}
