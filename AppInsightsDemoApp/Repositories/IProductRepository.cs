using AppInsightsDemoApp.Models;
using Microsoft.EntityFrameworkCore;
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

        public ProductRepository(AppInsightsDemoDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            await Task.Delay(3000);
            return await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync() => 
            await _context.Products.ToListAsync();
    }
}
