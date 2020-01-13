using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Designeo.Eshop.Core.Entities;
using Designeo.Eshop.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Designeo.Eshop.Infrastructure.Data
{
    public class OrderService : IOrderService
    {
        private readonly EshopContext _context;

        public OrderService(EshopContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();
        }

        public Task<Order> GetByIdAsync(long id)
        {
            return _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _context.OrderItems.RemoveRange(order.Items);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
        
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        
        public bool OrderExists(long id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
