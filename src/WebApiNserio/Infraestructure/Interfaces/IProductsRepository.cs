using Infraestructure.Dtos.Response;

namespace Infraestructure.Interfaces
{
    public interface IProductsRepository
    {
        public Task<List<GetProudctsResponseDto>> GetProuducts();
    }
}