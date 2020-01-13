using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Designeo.Eshop.Core.Entities;

namespace Designeo.Eshop.Core.Interfaces
{
    public interface IOrderService
    {
        Task<IReadOnlyCollection<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(long id);
        Task AddAsync(Order order);
        bool OrderExists(long id);
        Task DeleteAsync(Order order);
        Task CommitAsync();
    }
}
