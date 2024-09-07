//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ImageManipulation.Data.Services
//{
   
//    public class FileService(IWebHostEnvironment environment) : IFileService
//    {
//        public void DeleteImage(string imageFileName)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<string> SaveImageAsync(IFormFile imageFile, string[] allowedFileExtensions, string organizerId)
//        {
//            if (imageFile == null)
//            {
//                throw new ArgumentNullException(nameof(imageFile));
//            }
//            var contentPath = environment.ContentRootPath;
//            var path = Path.Combine(contentPath, "Uploads");

//            if (!Directory.Exists(path))
//            {
//                Directory.CreateDirectory(path);
//            }

//            var ext = Path.GetExtension(imageFile.FileName);
//            if (!allowedFileExtensions.Contains(ext))
//            {
//                throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");
//            }

//            var fileName = $"ProfilePic_{organizerId}_{DateTime.UtcNow}{ext}";
//            var fileNameWithPath = Path.Combine(path, fileName);
//            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
//            await imageFile.CopyToAsync(stream);
//            return fileName;
//        }

       
//    }
//}
