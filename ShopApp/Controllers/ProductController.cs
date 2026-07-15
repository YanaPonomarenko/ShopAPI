using Microsoft.AspNetCore.Mvc;
using ShopApp.Filters;
using ShopApp.Interfaces;
using ShopDomain.Models;

namespace ShopApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [LogActionFilter]

    public class ProductController(IProductService _productService) : ControllerBase
    {

        [HttpGet]
        public List<Product> GetProducts()
        {
            return _productService.GetAllProducts();
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById([FromRoute] int id)
        {
            var product = new Product()
            {
                Title = $"Test Product {id}",
                Price = 100
            };
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddNewProduct([FromBody] Product product)
        {
            _productService.AddProduct(product);
            return Created();
        }
    }
}
