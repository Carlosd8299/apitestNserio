using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order
    {
        public int Orderid;
        public int Custid;
        public int Empid;
        public DateTime Orderdate;
        public DateTime Requireddate;
        public DateTime Shippeddate;
        public int Shipperid;
        public decimal Freight;
        public string Shipname;
        public string Shipaddress;
        public string Shipcity;
        public string Shipregion;
        public string Shippostalcode;
        public string Shipcountry;
    }
}
