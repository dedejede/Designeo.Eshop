using System.Collections.Generic;
using System.Linq;

namespace Designeo.Eshop.Api.Models.DTOs
{
    public class OrderDto
    {
        public long Id { get; set; }

        public string Note { get; set; } = string.Empty;

        public decimal Total { get; set; }

        public string State { get; set; }

        public IEnumerable<OrderItemDto> Items { get; set; } = Enumerable.Empty<OrderItemDto>();

    }
}
