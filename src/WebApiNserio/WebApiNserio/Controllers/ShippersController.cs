using Infraestructure.Interfaces;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiNserio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private IShippersRepository _repository;

        public ShippersController(IShippersRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<ShippersController>
        [HttpGet("ShippersList")]
        public async Task<IActionResult> Get()
        {
            return StatusCode(StatusCodes.Status200OK, await _repository.GetShippers());
        }

    }
}
