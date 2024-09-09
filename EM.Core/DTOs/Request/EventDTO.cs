using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Request
{
    public class EventDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [JsonPropertyName("performer_id")]
        public int PerformerId { get; set; }
        [JsonPropertyName("venue_id")]
        public int VenueId { get; set; }
        [JsonPropertyName("start_datetime")]
        public DateTime StartDateTime { get; set; }
        [JsonPropertyName("end_datetime")]
        public DateTime EndDateTime { get; set; }
    }
}
