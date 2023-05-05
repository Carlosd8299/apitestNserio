using Infraestructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiNserio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeesRepository _employeesRepository;

        public EmployeesController(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        // GET: api/<EmployeesController>
        [HttpGet("EmployeesList")]
        public async Task<IActionResult> Get()
        {
            return StatusCode(StatusCodes.Status200OK, await _employeesRepository.GetEmployees());
        }

    }
}
