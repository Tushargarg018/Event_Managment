using EM.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
        public interface IFileService
        {
            public byte[] GetImageAsByteArray(string RequestPath);
            Task<string> SaveImageAsync(IFormFile imageFile, string organizerId);
            void DeleteImage(string imageFileName);            
            Task<string> UploadEventDocument(IFormFile imageFile, int eventId, int type);
            Task<string> UpdateImageAsync(string base64String, int organizer_id, int performer_id);
            Task<string> SaveImageFromBase64(string base64String, int organizer_id, int documentType);

    }
}
