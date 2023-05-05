using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infraestructure.Dtos.Request
{
    public class CreateOrderRequestDto
    {
        [Required]
        public int empid { get; set; }
        [Required]
        public int shipperid { get; set; }
        [Required]
        public int productid { get; set; }
        [Required]
        public int qty { get; set; }
        public decimal discount { get; set; }
        [Required]
        public decimal unitprice { get; set; }
        [Required]
        public decimal freight { get; set; }
        [Required]

        public string shipname { get; set; }
        [Required]
        public string shipaddress { get; set; }
        [Required]
        public string shipcity { get; set; }
        [Required]
        public string shipcountry { get; set; }
        [Required]

        public DateTime orderdate { get; set; }
        [Required]

        public DateTime requireddate { get; set; }
        [Required]
        public DateTime shippeddate { get; set; }

    }
}
