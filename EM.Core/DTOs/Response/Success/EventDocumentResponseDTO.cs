using EM.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Response.Success
{
    public class EventDocumentResponseDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Document Type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// event id
        /// </summary>
        [JsonPropertyName("event_id")]
        public int EventId { get; set; }

        /// <summary>
        /// title of the document
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// path of the document
        /// </summary>
        [JsonPropertyName("file_path")]
        public  string FilePath { get; set; }

        /// <summary>
        /// Creation date of the document
        /// </summary>
        [JsonPropertyName("created_on")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Modified date of the document
        /// </summary>
        [JsonPropertyName("modified_on")]
        public DateTime ModifiedOn { get; set; }

    }
}
