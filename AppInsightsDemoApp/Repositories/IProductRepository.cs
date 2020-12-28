using AppInsightsDemoApp.Logging;
using AppInsightsDemoApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppInsightsDemoApp.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly AppInsightsDemoDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(AppInsightsDemoDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            using var _ =_logger.LogMethod();
            await Task.Delay(3000);
            return await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            using var _ = _logger.LogMethod();
            return await _context.Products.ToListAsync();
        }
    }
}
