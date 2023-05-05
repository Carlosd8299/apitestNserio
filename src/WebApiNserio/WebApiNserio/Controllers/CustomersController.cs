using Infraestructure;
using Infraestructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiNserio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public ICustomerRepository _customerSqlRepository;

        public CustomersController(ICustomerRepository customerSqlRepository)
        {
            _customerSqlRepository = customerSqlRepository;
        }


        // GET: api/<CustomersController>
        [HttpGet("PredictedOrderForCustomers")]
        public async Task<ActionResult> Get()
        {
            return StatusCode(StatusCodes.Status200OK, await _customerSqlRepository.GetCustomerPredictedDate());
        }
    }
}
