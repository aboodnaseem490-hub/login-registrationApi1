using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SupportTicketingSystem.Data.Entities;

namespace SupportTicketingSystem.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetActiveProductsAsync();
    }
}
