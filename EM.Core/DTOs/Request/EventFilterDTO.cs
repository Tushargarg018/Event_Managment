using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EM.Core.Enums;

namespace EM.Core.DTOs.Request
{
    public class EventFilterDTO
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("start_datetime")]
        public DateTime? StartDateTime { get; set; }

        [JsonPropertyName("end_datetime")]
        public DateTime? EndDateTime { get; set; }

        [JsonPropertyName("organizer_id")]
        public int? OrganizerId { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("sort")]
        public string? Sort { get; set; }

        [JsonPropertyName("sort_by")]
        public string? SortBy { get; set; }
    }
}
