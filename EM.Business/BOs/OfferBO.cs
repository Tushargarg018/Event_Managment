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
        public int EventId { get; set; }
        public string? Type { get; set; }
        [JsonPropertyName("type_id")]
        public int TypeId { get; set; }
        public decimal Discount { get; set; }
        public int TotalOffers { get; set; }
        public int GroupSize { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
