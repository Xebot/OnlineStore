using Microsoft.AspNetCore.Http;
using OnlineStore.Contracts.Images;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Images.Services
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, CancellationToken cancellation);

        Task<ImageDto> GetImageDtoAsync(int id, CancellationToken cancellation);

        string[] GetImagesUrls(ProductImage[] images);

        Task<IReadOnlyCollection<string>> SaveImagesAsync(List<IFormFile> ImageFiles, CancellationToken cancellation);

        Task<ProductImage[]> SaveProductImagesAsync(string[] imagesUrls, Product product, CancellationToken cancellation);
    }
}
