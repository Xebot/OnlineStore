using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OnlineStore.AppServices.Common.DateTimeProviders;
using OnlineStore.AppServices.Common.Events.Common;
using OnlineStore.AppServices.Common.NotificationServices;
using OnlineStore.AppServices.Images.Services;
using OnlineStore.AppServices.Products.Models;
using OnlineStore.AppServices.Products.Repositories;
using OnlineStore.Contracts.Common;
using OnlineStore.Contracts.Enums;
using OnlineStore.Contracts.Products;
using OnlineStore.Domain.Entities;

namespace OnlineStore.AppServices.Products.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEventAccumulator _eventContainer;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IImageService _imageService;
        private readonly INotificationService _notificationService;

        public ProductService(
            IProductRepository repository,
            IMapper mapper,
            IEventAccumulator eventContainer,
            IDateTimeProvider dateTimeProvider,
            IImageService imageService,
            INotificationService notificationService)
        {
            _repository = repository;
            _mapper = mapper;
            _eventContainer = eventContainer;
            _dateTimeProvider = dateTimeProvider;
            _imageService = imageService;
            _notificationService = notificationService;
        }

        public async Task AddProductAsync(ShortProductDto productDto, IFormFile imageFile, CancellationToken cancellationToken)
        {
            var imageId = await _imageService.SaveImageAsync(imageFile, cancellationToken);

            var domainProduct = _mapper.Map<Product>(productDto);
            domainProduct.ImageUrl = $"https://localhost:7194/images/{imageId}";

            await _repository.AddAsync(domainProduct);
        }

        public async Task<ProductsListDto> GetProductsAsync(PagedRequest request, CancellationToken cancellation)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var totalCount = await _repository.GetProductsTotalCountAsync(cancellation);

            if (totalCount == 0)
            {
                return new ProductsListDto
                {
                    PageNumber = 1,
                    TotalCount = totalCount,
                    PageSize = 1,
                    Result = []
                };
            }

            var products = await _repository.GetProductsAsync(new GetProductsRequest
            {
                Take = request.PageSize,
                Skip = (request.PageNumber - 1) * request.PageSize,
                IncludeCategory = true
            }, cancellation);

            var productList = _mapper.Map<List<ShortProductDto>>(products);

            return new ProductsListDto
            {
                PageNumber = request.PageNumber,
                PageSize= request.PageSize,
                TotalCount = totalCount,
                Result = productList
            };
        }
    }
}
