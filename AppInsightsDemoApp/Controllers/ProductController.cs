using AppInsightsDemoApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AppInsightsDemoApp.Repositories;
using Microsoft.Extensions.Logging;
using AppInsightsDemoApp.Logging;

namespace AppInsightsDemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            using var _ = _logger.LogMethod();
            return await _productRepository.GetProductsAsync();
        }

        [HttpGet]
        [Route("{productId:int}")]
        public async Task<Product> GetById(int productId)
        {
            using var _ = _logger.LogMethod();
            await Task.Delay(3000);
            return await _productRepository.GetProductByIdAsync(productId);
        }
    }
}