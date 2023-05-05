using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Setings
{
    public class SqlSettings
    {
        public string ConnectionString { get; set; }
        public string SpSalesPredictedDate { get; set; }
        public string SpGetClientOrders { get; set; }

        public string SpGetShippers { get; set; }

        public string SpGetEmployees { get; set; }

        public string SpGetProducts { get; set; }

        public string SpAddNewOrder { get; set; }



    }
}
