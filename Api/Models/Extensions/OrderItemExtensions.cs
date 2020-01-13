using System.Collections.Generic;
using System.Linq;
using Designeo.Eshop.Api.Models.DTOs;
using Designeo.Eshop.Core.Entities;

namespace Designeo.Eshop.Api.Models.Extensions
{
    internal static class OrderItemExtensions
    {
        internal static OrderItemDto ToOrderItemDto(this OrderItem item)
        {
            return new OrderItemDto()
            {
                Id = item.Id,
                Price = item.Price,
                Name = item.Name
            };
        }

        internal static OrderItem ToOrderItem(this OrderItemDto itemDto)
        {
            return new OrderItem()
            {
                Price = itemDto.Price,
                Name = itemDto.Name,
            };
        }

        internal static IEnumerable<OrderItemDto> ToOrderItemsDto(this IEnumerable<OrderItem> items)
        {
            return items.Select(item => item.ToOrderItemDto());
        }

        internal static IEnumerable<OrderItem> ToOrderItems(this IEnumerable<OrderItemDto> itemsDto)
        {
            return itemsDto.Select(itemDto => itemDto.ToOrderItem());
        }

    }
}
