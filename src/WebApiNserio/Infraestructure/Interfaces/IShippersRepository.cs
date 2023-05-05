using Infraestructure.Dtos.Response;

namespace Infraestructure.Interfaces
{
    public interface IShippersRepository
    {
        public Task<List<GetShippersResponseDto>> GetShippers();
    }
}