using Infraestructure.Dtos.Response;

namespace Infraestructure.Interfaces
{
    public interface IEmployeesRepository
    {
        public Task<List<GetEmployeesResponseDto>> GetEmployees();
    }
}