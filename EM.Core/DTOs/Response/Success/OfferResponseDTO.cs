using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EM.Core.Enums;

namespace EM.Core.DTOs.Response.Success
{
    public class OfferResponseDTO
    {
       
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }

        [JsonPropertyName("total_offers")]
        public int TotalOffers { get; set; }

        [JsonPropertyName("group_size")]
        public int GroupSize { get; set; }

        [JsonPropertyName("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("modified_on")]
        public DateTime ModifiedOn { get; set; }
    }
}
