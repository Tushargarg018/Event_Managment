using EM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Response.Success
{
    public class VenueResponseDTO
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public VenueTypeEnum Type { get; set; }

        [JsonPropertyName("type_id")]
        public int TypeId { get; set; }

        [JsonPropertyName("max_capacity")]
        public int MaxCapacity { get; set; }

        [JsonPropertyName("address_line_1")]
        public string AddressLine1 { get; set; }

        [JsonPropertyName("address_line_2")]
        public string AddressLine2 { get; set; }

        [JsonPropertyName("zip_code")]
        public int ZipCode { get; set; }

        [JsonPropertyName("city_id")]
        public int CityId { get; set; }

		[JsonPropertyName("city_name")]
		public string? CityName { get; set; }

		[JsonPropertyName("state_id")]
        public int StateId { get; set; }

		[JsonPropertyName("state_name")]
		public string? StateName { get; set; }

		[JsonPropertyName("country_id")]
        public int CountryId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("modified_on")]
        public DateTime ModifiedOn { get; set; }


    }
}