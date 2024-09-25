using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Response.Success
{
    public class TaxDetailDTO
    {
        [JsonPropertyName("tax_details")]
        public required string TaxDetails { get; set; }
    }
}
