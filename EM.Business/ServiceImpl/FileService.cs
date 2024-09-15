using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Business.Services;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Hosting;
using EM.Core.Enums;
using static System.Net.Mime.MediaTypeNames;
namespace EM.Business.ServiceImpl
{ 
    public class FileService : IFileService
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;
        private readonly string[] _allowedExtensions;
        private readonly string _logoPath;
        private readonly string _bannerPath;

        public FileService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.environment = environment;
            _allowedExtensions = configuration.GetSection("FileSettings:AllowedExtensions").Get<string[]>();
            _logoPath = configuration["FileSettings:EventDocumentPaths:Logo"];
            _bannerPath = configuration["FileSettings:EventDocumentPaths:Banner"];
        }


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

        public async Task<string> SaveImageAsync(IFormFile imageFile, string[] allowedFileExtensions, string organizerId, string baseUrl)
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

            int startIndex = Array.IndexOf(parts, "Uploads/profile");
            string imagePath = string.Join("\\", parts, startIndex, parts.Length - startIndex);
            string responsePath = baseUrl + imagePath;
            string url = responsePath.Replace('\\', '/');
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);
            return url;
        }
        /// <summary>
        /// Upload document for the event
        /// </summary>
        /// <param name="eventDocument"></param>
        /// <param name="id"></param>
        /// <param name="documentType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<string> UploadEventDocument(IFormFile eventDocument,int id, int documentType)
        {
            if (eventDocument == null) throw new ArgumentNullException(nameof(eventDocument));
            string identifier = id.ToString();
            string contentPath = environment.ContentRootPath;
            string folderPath = documentType == 0 ? _logoPath : _bannerPath;
            string fullPath = Path.Combine(contentPath, folderPath);
            EnsureDirectoryExists(fullPath);
            var extension = Path.GetExtension(eventDocument.FileName);
            ValidateFileExtension(extension, _allowedExtensions);
            string fileName = $"{identifier}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}{extension}";
            string filePathWithFullName = Path.Combine(fullPath, fileName);

            using var stream = new FileStream(filePathWithFullName, FileMode.Create);
            await eventDocument.CopyToAsync(stream);

            return GenerateUrl(folderPath, filePathWithFullName);

        }
        /// <summary>
        /// Check and create the directory
        /// </summary>
        /// <param name="path"></param>
        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        /// <summary>
        /// Validate the allowed extension
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="allowedExtensions"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void ValidateFileExtension(string extension, string[] allowedExtensions)
        {
            if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException($"Only {string.Join(", ", allowedExtensions)} are allowed.");
            }
        }
        /// <summary>
        /// return the required path
        /// </summary>
        /// <param name="directoryName"></param>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        private static string GenerateUrl(string directoryName, string fullPath)
        {
            string[] parts = fullPath.Split('\\');
            int startIndex = Array.IndexOf(parts, directoryName);
            string relativePath = string.Join("\\", parts, startIndex, parts.Length - startIndex);
            return relativePath.Replace('\\', '/');
        }
    }
}
