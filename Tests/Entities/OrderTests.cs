using Designeo.Eshop.Core.Entities;
using Designeo.Eshop.Core.Enums;
using NUnit.Framework;

namespace Designeo.Eshop.Tests.Entities
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void OrderTotal_GivesSumOfItems()
        {
            var order = new Order();
            order.Items.Add(new OrderItem(){Price = 10m});
            order.Items.Add(new OrderItem(){Price = 20m});

            Assert.That(order.Total(), Is.EqualTo(30d));
        }

        [Test]
        public void OrderTotalWithNoItems_GivesZero()
        {
            var order = new Order();

            Assert.That(order.Total(), Is.EqualTo(0d));
        }

        [Test]
        public void NewOrder_IsInNewState()
        {
            var order = new Order();

            Assert.That(order.State, Is.EqualTo(OrderState.New));
        }

        [Test]
        public void NewOrder_CanBeCancelled()
        {
            var order = new Order();

            order.SetState(OrderState.Canceled);

            Assert.That(order.State, Is.EqualTo(OrderState.Canceled));
        }

        [Test]
        public void NewOrder_CanBeCompleted()
        {
            var order = new Order();

            order.SetState(OrderState.Completed);

            Assert.That(order.State, Is.EqualTo(OrderState.Completed));
        }

        [Test]
        public void CancelledOrder_CannotBeCompleted()
        {
            var order = new Order();
            order.SetState(OrderState.Canceled);

            order.SetState(OrderState.Completed);

            Assert.That(order.State, Is.EqualTo(OrderState.Canceled));
        }

        [Test]
        public void CompletedOrder_CannotBeCancelled()
        {
            var order = new Order();
            order.SetState(OrderState.Completed);

            order.SetState(OrderState.Canceled);

            Assert.That(order.State, Is.EqualTo(OrderState.Completed));
        }

    }
}