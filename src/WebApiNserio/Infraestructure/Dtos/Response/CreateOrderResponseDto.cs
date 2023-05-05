using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Dtos.Response
{
    public class CreateOrderResponseDto
    {
        public bool Success { get; set; } = false;
        public int Order { get; set; } = 0;
    }
}
