using Microsoft.AspNetCore.Mvc;
using OnlineStore.AppServices.Images.Services;

namespace OnlineStore.MVC.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("images/{imageId}")]
        public async Task<IActionResult> Get([FromRoute] int imageId, CancellationToken cancellation)
        {
            var image = await _imageService.GetImageDtoAsync(imageId, cancellation);

            return File(image.Data, image.ContentType);
        }
    }
}
