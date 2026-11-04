using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SupportTicketingSystem.Data.Entities;

namespace SupportTicketingSystem.Data.Repositories.ProductRepositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetActiveProductsAsync();
    }
}