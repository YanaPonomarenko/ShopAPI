using Microsoft.AspNetCore.Mvc;
using ShopDomain.Models;

namespace ShopApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase
    {
        [HttpGet]
        public Product GetProducts()
        {
            return new Product()
            {
                Title = "Milk",
                Price = 40.9f
            };
        }
    }
}
