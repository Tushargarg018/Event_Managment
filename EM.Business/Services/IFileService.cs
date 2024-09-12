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
            Task<string> SaveImageAsync(IFormFile imageFile, string[] allowedFileExtensions, string organizerId);
            void DeleteImage(string imageFileName);

            Task<string> SaveImage(IFormFile imageFile, string[] allowedFileExtensions, object Id , string directoryName);

        }
}
