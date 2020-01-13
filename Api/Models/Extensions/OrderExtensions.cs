using System.Collections.Generic;
using System.Linq;
using Designeo.Eshop.Api.Models.DTOs;
using Designeo.Eshop.Core.Entities;

namespace Designeo.Eshop.Api.Models.Extensions
{
    internal static class OrderExtensions
    {
        internal static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                Note = order.Note,
                State = order.State.ToString(),
                Total = order.Total(),
                Items = order.Items.ToOrderItemsDto(),
            };
        }

        public static Order ToOrder(this OrderDto orderDto, Order destinationOrder)
        {
            destinationOrder.Note = orderDto.Note;
            destinationOrder.Items = orderDto.Items?.ToOrderItems().ToList();

            return destinationOrder;
        }

        internal static Order ToOrder(this OrderDto orderDto)
        {
            return orderDto.ToOrder(new Order());
        }

        internal static IEnumerable<OrderDto> ToOrdersDto(this IEnumerable<Order> items)
        {
            return items.Select(i => i.ToOrderDto());
        }
    }
}
