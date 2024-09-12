using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Business.Services;
using Microsoft.Extensions.Configuration;

namespace EM.Business.ServiceImpl
{ 
    public class FileService : IFileService
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;

        public FileService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
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


        public async Task<string> SaveImage(IFormFile imageFile, string[] allowedFileExtensions, object Id , string directoryName)
        {
            //1. checking if the uploaded file is null then return exception
            if (imageFile == null)
            {
                throw new ArgumentNullException(nameof(imageFile));
            }

            //2.Convert the id to a string so that we can add it to path for identification
            string Identifier = Id.ToString();

            //3. Get base directory for storing files
            var contentpath = environment.ContentRootPath;

            //4. Combines the content root path with the directoryName subdirectory to create the full path where the image will be saved.
            var path = Path.Combine(contentpath , directoryName);

            //5. check if directory exist for path or not if no then create one
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //6. first extract extension and then check if the extension given is allowed in allowedfileExtension or not like .jp or .jpeg
            var ext = Path.GetExtension(imageFile.FileName);
            if (!allowedFileExtensions.Contains(ext))
            {
                throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");
            }

            //7.create a unique file name for the image having identifier , datetime and extension
            var fileName = $"{Identifier}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()}{ext}";

            //8. crate fileName with path also
            var fileNamewithPath = Path.Combine (path, fileName);

            //9. Create the file steam where FileSteam Constructor is called with file path and create new file it if it doesnot exist or overwrite it
            using var steam =  new FileStream(fileNamewithPath , FileMode.Create);
            //10.copy img to path
            await imageFile.CopyToAsync (steam);

            //11.// Generate the URL for accessing the file
            string[] parts = fileNamewithPath.Split('\\');
            int startIndex = Array.IndexOf(parts, directoryName);
            var baseUrl = configuration["Appsettings:BaseUrl"];
            string imagePath = string.Join("\\", parts, startIndex, parts.Length - startIndex);
            string url = $"{baseUrl}{imagePath.Replace('\\', '/')}";

            return url;
        }
    }
}
