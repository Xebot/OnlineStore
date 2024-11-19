using AutoMapper;
using Moq;
using OnlineStore.AppServices.Common.DateTimeProviders;
using OnlineStore.AppServices.Common.Events.Common;
using OnlineStore.AppServices.Common.NotificationServices;
using OnlineStore.AppServices.Images.Services;
using OnlineStore.AppServices.Products.Models;
using OnlineStore.AppServices.Products.Repositories;
using OnlineStore.AppServices.Products.Services;
using System.Runtime.Intrinsics.Arm;
using Xunit;

namespace OnlineStore.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IEventAccumulator> _eventAccumulatorMock = new Mock<IEventAccumulator>();
        private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        private readonly Mock<IImageService> _imageServiceMock = new Mock<IImageService>();
        private readonly Mock<INotificationService> _notificationService = new Mock<INotificationService>();

        //[Fact]
        //public async Task GetProduct_ShouldReturnCorrectProduct()
        //{
        //    // Arrange
        //    var request = new GetProductsRequest
        //    {
        //        IncludeCategory = true,
        //    };

        //    _productRepositoryMock.Setup(repo => repo.GetProductsAsync(request))
        //    .Returns(Task.FromResult(new List<Domain.Entities.Product>
        //    {
        //        new Domain.Entities.Product
        //        {
        //            Id = 1, Name = "name1"                    
        //        },
        //        new Domain.Entities.Product
        //        {
        //            Id = 2, Name = "name2"
        //        }
        //    }));

        //    var productService = new ProductService(
        //        _productRepositoryMock.Object,
        //        _mapperMock.Object,
        //        _eventAccumulatorMock.Object,
        //        _dateTimeProviderMock.Object);

        //    // Act
        //    var products = await productService.GetProductsAsync(request);

        //    // Assert
        //    Assert.NotNull(products);
        //    Assert.Equal(2, products.Count);
        //}

        [Fact]
        public async Task GetProduct_ThrowArgumentNullExceptionIfRequestIsNull()
        {
            var productService = new ProductService(
                _productRepositoryMock.Object,
                _mapperMock.Object,
                _eventAccumulatorMock.Object,
                _dateTimeProviderMock.Object,
                _imageServiceMock.Object,
                _notificationService.Object);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => productService.GetProductsAsync(null, CancellationToken.None));
        }
    }
}
