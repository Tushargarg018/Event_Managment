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
            Task<string> SaveImageAsync(IFormFile imageFile, string[] allowedFileExtensions, string organizerId, string baseUrl);
            void DeleteImage(string imageFileName);            
            Task<string> UploadEventDocument(IFormFile imageFile, int eventId, int type);
    }
}
