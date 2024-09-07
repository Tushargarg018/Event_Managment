//using EM.Business.Services;
//using EM.Core.DTOs.Request;
//using EM.Data.Entities;
//using EM.Data.Repositories;
//using ImageManipulation.Data.Models.DTOs;
//using ImageManipulation.Data.Repositories;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;

//namespace EM.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ImageController : ControllerBase
//    {
//        private readonly IFileService fileService;
//        private readonly IProductRepository productRepository;

//        public ImageController(IFileService fservice, IProductRepository productRepo)
//        {
//            fileService = fservice;
//            productRepository = productRepo;
//        }


//        [HttpPost]
//        [Route("performer")]
//        public async Task<IActionResult> CreateProduct(PerformerDTO productToAdd)
//        {
//            try
//            {
//                if (productToAdd.ImageFile?.Length > 1 * 1024 * 1024)
//                {
//                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
//                }
//                string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
//                string createdImageName = await fileService.SaveImageAsync(productToAdd.ImageFile, allowedFileExtentions, productToAddBO.OrganizerId);

//                // mapping `ProductDTO` to `Product` manually. You can use automapper.
//                var product = new Performer
//                {
//                    Profile = createdImageName
//                };
//                //var createdProduct = await productRepository.AddPerformerImageAsync(product);
//                //return CreatedAtAction(nameof(CreateProduct), createdProduct);
//                return Ok("OKK");
//            }
//            catch (Exception ex)
//            {
//                //logger.LogError(ex.Message);
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }
//    }
//}
