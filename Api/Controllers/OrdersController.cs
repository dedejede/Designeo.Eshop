using System.Collections.Generic;
using System.Threading.Tasks;
using Designeo.Eshop.Api.Models.DTOs;
using Designeo.Eshop.Api.Models.Extensions;
using Designeo.Eshop.Core.Entities;
using Designeo.Eshop.Core.Enums;
using Designeo.Eshop.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Designeo.Eshop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Orders
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders.ToOrdersDto());
        }

        // GET: api/Orders/5
        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(long), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> GetOrder(long orderId)
        {
            Order order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
            {
                return NotFound(orderId);
            }

            return order.ToOrderDto();
        }

        // POST: api/Orders
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto orderDto)
        {
            Order order = orderDto.ToOrder();
            await _orderService.AddAsync(order);

            return CreatedAtAction(nameof(GetOrder), new { orderId = order.Id }, order.ToOrderDto());
        }
        
        // DELETE: api/Orders/5
        [HttpDelete("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(long), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> DeleteOrder(long orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
            {
                return NotFound(orderId);
            }

            await _orderService.DeleteAsync(order);
            return order.ToOrderDto();
        }

        // PUT: api/Orders/5
        [HttpPut("{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(long), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrder(long orderId, OrderDto orderDto)
        {
            Order order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
            {
                return NotFound(orderId);
            }

            orderDto.ToOrder(order);

            await _orderService.CommitAsync();
            return NoContent();
        }

        // PUT: api/Orders/5/Cancel
        [HttpPut("{orderId}/[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(long), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Cancel(long orderId)
        {
            return await SetState(orderId, OrderState.Canceled);
        }

        // PUT: api/Orders/5/Cancel
        [HttpPut("{orderId}/[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(long), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Complete(long orderId)
        {
            return await SetState(orderId, OrderState.Completed);
        }

        private async Task<IActionResult> SetState(long orderId, OrderState state)
        {
            Order order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
            {
                return NotFound(orderId);
            }

            bool success = order.SetState(state);
            if (!success)
            {
                return BadRequest();
            }
            
            await _orderService.CommitAsync();
            return NoContent();
        }

    }
}
