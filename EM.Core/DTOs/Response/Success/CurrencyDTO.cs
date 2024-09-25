using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Response.Success
{
    public class CurrencyDTO
    {
        public int Id { get; set; }
        [JsonPropertyName("country_id")]
        public int CountryId { get; set; }
        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
        public string Symbol { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
    }
}
