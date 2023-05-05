using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure
{
    public class InfraestructureException : Exception
    {
        public object Detail { get; set; }

        public InfraestructureException(object detail)
        {
            Detail = detail;
        }
    }
}
