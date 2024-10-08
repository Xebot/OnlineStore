using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineStore.AppServices.Common.DateTimeProviders;
using OnlineStore.AppServices.Common.Events.Common;
using OnlineStore.AppServices.Common.NotificationServices;
using OnlineStore.AppServices.Products.Models;
using OnlineStore.AppServices.Products.Repositories;
using OnlineStore.Contracts.Products;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Events;

namespace OnlineStore.AppServices.Products.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEventAccumulator _eventContainer;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ProductService(
            IProductRepository repository,
            IMapper mapper,
            IEventAccumulator eventContainer,
            IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _mapper = mapper;
            _eventContainer = eventContainer;
            _dateTimeProvider = dateTimeProvider;
        }

        public Task AddProductAsync(ShortProductDto productDto, CancellationToken cancellationToken)
        {
            var domainProduct = _mapper.Map<Product>(productDto);

            _eventContainer.AddEvent(new AddProductEvent
            {
                EventDate = _dateTimeProvider.UtcNow,
                ProductName = "ProductName"
            });

            return Task.CompletedTask;

            return _repository.AddAsync(domainProduct);
        }

        public Task<List<Product>> GetProductsAsync(GetProductsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }



            return _repository.GetProductsAsync(request);
        }

        public bool IsBussinessDay()
        {
            var today = _dateTimeProvider.UtcNow;

            return today.DayOfWeek != DayOfWeek.Saturday && today.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}
