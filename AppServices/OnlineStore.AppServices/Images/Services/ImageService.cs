using Microsoft.AspNetCore.Http;
using OnlineStore.AppServices.Images.Repositories;
using OnlineStore.Contracts.Images;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Images.Services
{
    public sealed class ImageService : IImageService
    {
        private readonly IImageRepository _repository;
        public ImageService(
            IImageRepository repository)
        {
            _repository = repository;
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

        public async Task<int> SaveImageAsync(IFormFile imageFile, CancellationToken cancellation)
        {
            var productImage = new ProductImage
            {
                Name = imageFile.FileName,
                Content = await GetByteArrayAsync(imageFile, cancellation),
                ContentType = imageFile.ContentType
            };

            return await _repository.SaveAsync(productImage, cancellation);
        }

        private async Task<byte[]> GetByteArrayAsync(IFormFile imageFile, CancellationToken cancellation)
        {
            using var memoryStream = new MemoryStream();
            await imageFile.CopyToAsync(memoryStream, cancellation);

            return memoryStream.ToArray();
        }
    }
}
