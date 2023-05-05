using Infraestructure;
using Infraestructure.Dtos.Request;
using Infraestructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiNserio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public IOrdersRepository _ordersRepository;

        public OrdersController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        // GET api/<OrdersController>/5
        [HttpGet("OrdersByClient/{id}")]

        public async Task<ActionResult> Get(int id)
        {
            return StatusCode(StatusCodes.Status200OK, await _ordersRepository.GetOrdersByClient(id));

        }

        // POST api/<OrdersController>
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> Post([FromBody] CreateOrderRequestDto requestDto)
        {
            var result = await _ordersRepository.CreateOrder(requestDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);


        }

    }
}
