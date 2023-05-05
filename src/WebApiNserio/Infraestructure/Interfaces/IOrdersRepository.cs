using Domain.Models;
using Infraestructure.Dtos.Request;
using Infraestructure.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IOrdersRepository
    {
        public Task<List<GetOrdersByClientResponseDTO>> GetOrdersByClient(int idCustomer);
        public Task<CreateOrderResponseDto> CreateOrder(CreateOrderRequestDto requestDto);
    }
}
