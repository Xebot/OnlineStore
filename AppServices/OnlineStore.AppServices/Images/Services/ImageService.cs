using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OnlineStore.AppServices.Images.Repositories;
using OnlineStore.Contracts.Images;
using OnlineStore.Domain.Entities;

namespace OnlineStore.AppServices.Images.Services
{
    public sealed class ImageService : IImageService
    {
        private readonly IImageRepository _repository;
        private readonly ILogger<ImageService> _logger;

        public ImageService(
            IImageRepository repository,
            ILogger<ImageService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ImageDto> GetImageDtoAsync(int id, CancellationToken cancellation)
        {
            var image = await _repository.GetAsync(id);

            return new ImageDto
            {
                ContentType = image.ContentType,
                Data = image.Content
            };
        }

        public string[] GetImagesUrls(ProductImage[] images)
        {
            if(images.Length == 0)
            {
                return [];
            }

            var urls = new List<string>(images.Length);
            foreach(var image in images)
            {
                urls.Add($"/images/{image.Id}");
            }

            return [.. urls];
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile, CancellationToken cancellation)
        {
            var productImage = new ProductImage
            {
                Name = imageFile.FileName,
                Content = await GetByteArrayAsync(imageFile, cancellation),
                ContentType = imageFile.ContentType
            };

            var imageId = await _repository.SaveAsync(productImage, cancellation);

            return $"/images/{imageId}";
        }

        public async Task<IReadOnlyCollection<string>> SaveImagesAsync(List<IFormFile> ImageFiles, CancellationToken cancellation)
        {
            var result = new List<string>(ImageFiles.Count);
            foreach (var imageFile in ImageFiles)
            {
                var imageUrl = await SaveImageAsync(imageFile, cancellation);
                result.Add(imageUrl);
            }

            return result.ToArray();
        }

        public async Task<ProductImage[]> SaveProductImagesAsync(string[] imagesUrls, Product product, CancellationToken cancellation)
        {
            if (imagesUrls.Length == 0)
            {
                return [];
            }

            var result = new List<ProductImage>(imagesUrls.Length);
            foreach (var imageUrl in imagesUrls)
            {
                var imageId = ExtractImageId(imageUrl);
                var existingImage = await _repository.GetAsync(imageId);

                if (existingImage == null)
                {
                    _logger.LogError("Не найдена картинка с Id = {imageId}", imageId);
                    throw new Exception();//TODO: подумать
                }

                existingImage.Product = product;

                result.Add(existingImage);
            }

            return result.ToArray();
        }

        private static int ExtractImageId(string url)
        {
            int lastSlashIndex = url.LastIndexOf('/');
            if (lastSlashIndex != -1 && lastSlashIndex + 1 < url.Length)
            {
                var stringId = url.Substring(lastSlashIndex + 1);

                return int.Parse(stringId);
            }
            throw new Exception();//TODO: подумать
        }

        private async Task<byte[]> GetByteArrayAsync(IFormFile imageFile, CancellationToken cancellation)
        {
            using var memoryStream = new MemoryStream();
            await imageFile.CopyToAsync(memoryStream, cancellation);

            return memoryStream.ToArray();
        }
    }
}
