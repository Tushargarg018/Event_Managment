using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EM.Business.BOs
{
    public class TaxDetailBO
    {
        public required JsonDocument TaxDetails { get; set; }
    }
}
