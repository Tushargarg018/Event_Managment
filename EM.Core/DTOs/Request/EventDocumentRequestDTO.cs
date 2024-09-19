using EM.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Request
{
    public class EventDocumentRequestDTO
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Document Type
        /// </summary>
        [JsonPropertyName("type")]
        public required DocumentType Type { get; set; }
        /// <summary>
        /// title of the document
        /// </summary>
        [JsonPropertyName("title")]
        public required string Title { get; set; }

        /// <summary>
        /// Base64 Encoded String
        /// </summary>
        
        [JsonPropertyName("file_string")]
        public required string Base64String { get; set; }
        
    }
}
