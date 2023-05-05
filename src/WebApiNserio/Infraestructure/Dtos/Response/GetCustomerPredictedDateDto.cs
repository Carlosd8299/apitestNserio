using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Dtos.Response
{
    public class GetCustomerPredictedDateDto
    {
        public string CustomerName { get; set; }
        public DateTime LastOrderDate { get; set; }
        public DateTime NextPredictedOrder { get; set; }
    }
}
