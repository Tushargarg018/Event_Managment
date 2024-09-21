using EM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Request
{
    public class OfferDTO
    {
        [JsonIgnore]
        public int EventId { get; set; }
        [JsonPropertyName("offer_id")]
        public int OfferId { get; set; }
        [JsonPropertyName("type")]
        public required OfferTypeEnum Type { get; set; }
        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("group_size")]
        public int GroupSize { get; set; }
    }
}
