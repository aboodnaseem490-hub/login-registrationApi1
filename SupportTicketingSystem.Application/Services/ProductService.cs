using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SupportTicketingSystem.Application.Interfaces;
using SupportTicketingSystem.Data.Entities;
using SupportTicketingSystem.Data.Repositories.ProductRepositories;

namespace SupportTicketingSystem.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetActiveProductsAsync()
        {
            return await _productRepository.GetActiveProductsAsync();
        }
    }
}