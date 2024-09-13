using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Request
{
    public class EventPriceCategoryRequestDTO
    {
        [JsonIgnore]
        public int EventId { get; set; }

        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]   
        public required string Name { get; set; }

        [JsonPropertyName("price")]
        public required int Price { get; set; }

        [JsonPropertyName("capacity")]
        public required int Capacity { get; set; }

    }
}

