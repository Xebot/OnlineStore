using Microsoft.AspNetCore.Http;
using OnlineStore.Contracts.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Images.Services
{
    public interface IImageService
    {
        Task<int> SaveImageAsync(IFormFile imageFile, CancellationToken cancellation);

        Task<ImageDto> GetImageDtoAsync(int id, CancellationToken cancellation);
    }
}
