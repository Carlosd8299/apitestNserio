using Infraestructure.Interfaces;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiNserio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            this._productsRepository = productsRepository;
        }

        // GET: api/<ProductsController>
        [HttpGet("ProductList")]
        public async Task<IActionResult> Get()
        {
            return StatusCode(StatusCodes.Status200OK, await _productsRepository.GetProuducts());
        }
    }
}
