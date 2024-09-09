using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Request
{
    public class PerformerDTO
    {
        public string Name { get; set; }
        public string Bio {  get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
