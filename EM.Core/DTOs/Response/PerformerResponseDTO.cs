using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Response
{
    public class PerformerResponseDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        [JsonPropertyName("profile_pic")]
        public string Profile { get; set; }
        [JsonPropertyName("created_on")]
        public DateTime CreatedOn { get; set; }
        [JsonPropertyName("modified_on")]
        public DateTime ModifiedOn { get; set; }
    }
}
