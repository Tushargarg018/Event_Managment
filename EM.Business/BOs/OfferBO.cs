using EM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Business.BOs
{
    public class OfferBO
    {
        public int Id { get; set; }
        [JsonPropertyName("event_id")]
        public int EventId { get; set; }
        public string Type { get; set; }
        public decimal Discount { get; set; }
        [JsonPropertyName("total_offers")]
        public int TotalOffers { get; set; }
        [JsonPropertyName("group_size")]
        public int GroupSize { get; set; }
        public int Status { get; set; }
        [JsonPropertyName("created_on")]
        public DateTime CreatedOn { get; set; }
        [JsonPropertyName("modified_on")]
        public DateTime ModifiedOn { get; set; }
    }
}
