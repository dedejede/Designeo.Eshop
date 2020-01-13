using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Designeo.Eshop.Api.Models.DTOs;
using Designeo.Eshop.Api.Models.Extensions;
using Designeo.Eshop.Core.Entities;
using Designeo.Eshop.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Designeo.Eshop.Api.Controllers
{
    [Route("api/Orders/{orderId}/Items")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderItemsController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST: api/Orders/5/Items
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(long), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddItems(long orderId, IEnumerable<OrderItemDto> itemsDto)
        {
            Order order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
            {
                return NotFound(orderId);
            }

            foreach (var itemDto in itemsDto)
            {
                order.Items.Add(itemDto.ToOrderItem());
            }

            await _orderService.CommitAsync();
            return NoContent();
        }

        // POST: api/Orders/5/Items
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(long), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddItems(long orderId, OrderItemDto itemsDto)
        {
            Order order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
            {
                return NotFound(orderId);
            }

            order.Items.Add(itemsDto.ToOrderItem());

            await _orderService.CommitAsync();
            return NoContent();
        }
        
        // DELETE: api/Orders/5/Items
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(long), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> DeleteAllAssociated(long orderId)
        {
            Order order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
            {
                return NotFound(orderId);
            }

            var deletedItems = order.Items.ToList();

            order.Items.Clear();
            await _orderService.CommitAsync();

            return Ok(deletedItems.ToOrderItemsDto());
        }

        // DELETE: api/Orders/5/Items/1
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(long), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderItemDto>> DeleteOne(long orderId, long id)
        {
            Order order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
            {
                return NotFound(orderId);
            }

            OrderItem item = order.Items.FirstOrDefault(p => p.Id == id); 
            if (item == null)
            {
                return NotFound(id);
            }

            order.Items.Remove(item);
            await _orderService.CommitAsync();

            return item.ToOrderItemDto();
        }

    }
}
