using AppInsightsDemoApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AppInsightsDemoApp.Repositories;

namespace AppInsightsDemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get() => 
            await _productRepository.GetProductsAsync();

        [HttpGet]
        [Route("{productId:int}")]
        public async Task<Product> GetById(int productId)
        {
            await Task.Delay(3000);
            return await _productRepository.GetProductByIdAsync(productId);
        }
    }
}