using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Business.Services;

namespace EM.Business.ServiceImpl
{ 
    public class FileService(IWebHostEnvironment environment) : IFileService
    {
        private byte[] ConvertImageToByteArray(string RequestPath)
        {
            var directory = Directory.GetCurrentDirectory();
            var parentDirectory = Directory.GetParent(directory).ToString();
            var path = Path.Combine(parentDirectory, RequestPath);
            return File.ReadAllBytes(path);
        }

        public byte[] GetImageAsByteArray(string RequestPath) =>
            ConvertImageToByteArray(RequestPath);
        public void DeleteImage(string imageFileName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile, string[] allowedFileExtensions, string organizerId)
        {
            if (imageFile == null)
            {
                throw new ArgumentNullException(nameof(imageFile));
            }
            var contentPath = environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads/profile");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ext = Path.GetExtension(imageFile.FileName);
            if (!allowedFileExtensions.Contains(ext))
            {
                throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");
            }

            var fileName = $"ProfilePic_{organizerId}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()}{ext}";
            var fileNameWithPath = Path.Combine(path, fileName);
            string[] parts = fileNameWithPath.Split('\\');

            int startIndex = Array.IndexOf(parts, "Uploads");
            string imagePath = string.Join("\\", parts, startIndex, parts.Length - startIndex);
            string responsePath = "http://localhost:5250\\" + imagePath;
            string url = responsePath.Replace('\\', '/');
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);
            return url;
        } 
    }
}
