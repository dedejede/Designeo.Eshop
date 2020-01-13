using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Designeo.Eshop.Core.Enums;

namespace Designeo.Eshop.Core.Entities
{
    public class Order
    {
        public long Id { get; set; }

        public string Note { get; set; } = string.Empty;

        public OrderState State { get; private set; } = OrderState.New;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        
        public decimal Total() => Items.Select(p => p.Price).Sum();

        public bool IsReadOnly => State == OrderState.Canceled || State == OrderState.Completed;

        public bool SetState(OrderState state)
        {
            bool canChangeState = state == OrderState.Canceled && CanCancel || 
                                  state == OrderState.Completed && CanComplete;

            if (canChangeState)
            {
                State = state;
                return true;
            }

            return false;
        }

        private bool CanCancel => State == OrderState.New || State == OrderState.Canceled;

        private bool CanComplete => State == OrderState.New || State == OrderState.Completed;
    }
}
