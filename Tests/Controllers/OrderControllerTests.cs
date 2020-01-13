using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Designeo.Eshop.Api.Controllers;
using Designeo.Eshop.Api.Models.DTOs;
using Designeo.Eshop.Core.Entities;
using Designeo.Eshop.Core.Enums;
using Designeo.Eshop.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Designeo.Eshop.Tests.Controllers
{
    [TestFixture]
    public class OrderControllerTests
    {

        [Test]
        public async Task GetAllOrders_GivesOkWithOrders()
        {
            var orderStub = new Order();
            orderStub.SetState(OrderState.Canceled);

            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock
                .Setup(m => m.GetAllAsync())
                .ReturnsAsync(Enumerable.Empty<Order>().ToList());

            var controller = new OrdersController(orderServiceMock.Object);
            var response = await controller.GetOrders();

            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var responseObject = ((OkObjectResult) response.Result).Value;
            Assert.IsInstanceOf<IEnumerable<OrderDto>>(responseObject);
        }

        [Test]
        public async Task CancelWithNewOrder_SetsState()
        {
            var orderStub = new Order();

            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock
                .Setup(p => p.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(orderStub);

            var controller = new OrdersController(orderServiceMock.Object);
            await controller.Cancel(orderId: 0);

            Assert.That(orderStub.State, Is.EqualTo(OrderState.Canceled));
        }

        [Test]
        public async Task CancelWithCompletedOrder_GivesBadResult()
        {
            var orderStub = new Order();
            orderStub.SetState(OrderState.Canceled);

            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock
                .Setup(p => p.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(orderStub);

            var controller = new OrdersController(orderServiceMock.Object);
            IActionResult response = await controller.Complete(orderId: 0);
            
            Assert.IsInstanceOf<BadRequestResult>(response);
        }

    }
}