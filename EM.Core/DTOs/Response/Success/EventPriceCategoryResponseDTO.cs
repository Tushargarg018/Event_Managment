using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Response.Success
{
    public class EventPriceCategoryResponseDTO
    {
        [JsonPropertyName("id")]
        public int Id {  get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("event_id")]
        public int EventId { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("modified_on")]
        public DateTime UpdatedOn { get; set; }
    }

}
