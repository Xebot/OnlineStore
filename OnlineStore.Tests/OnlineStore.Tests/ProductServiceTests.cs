using AutoMapper;
using Moq;
using OnlineStore.AppServices.Common.DateTimeProviders;
using OnlineStore.AppServices.Common.Events.Common;
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

        //[Fact]
        //public async Task GetProduct_ThrowArgumentNullExceptionIfRequestIsNull()
        //{
        //    var productService = new ProductService(
        //        _productRepositoryMock.Object,
        //        _mapperMock.Object,
        //        _eventAccumulatorMock.Object,
        //        _dateTimeProviderMock.Object);

        //    // Assert
        //    await Assert.ThrowsAsync<ArgumentNullException>(() => productService.GetProductsAsync(null));           

        //}

        //[Theory]
        //[InlineData("2024-10-7", true)]
        //[InlineData("2024-10-8", true)]
        //[InlineData("2024-10-9", true)]
        //[InlineData("2024-10-10", true)]
        //[InlineData("2024-10-11", true)]
        //[InlineData("2024-10-12", false)]
        //public void IsBusinessDay_ShoudReturnTrue_OnWeekday(DateTime dateTime, bool expected)
        //{
        //    _dateTimeProviderMock.Setup(dp => dp.UtcNow).Returns(dateTime);

        //    var productService = new ProductService(
        //        _productRepositoryMock.Object,
        //        _mapperMock.Object,
        //        _eventAccumulatorMock.Object,
        //        _dateTimeProviderMock.Object);

        //    var result = productService.IsBussinessDay();

        //    Assert.Equal(expected, result);
        //}

        //[Fact]
        //public void IsBusinessDay_ShoudReturnFalse_OnSaturday()
        //{
        //    _dateTimeProviderMock.Setup(dp => dp.UtcNow).Returns(new DateTime(2024, 10, 05));

        //    var productService = new ProductService(
        //        _productRepositoryMock.Object,
        //        _mapperMock.Object,
        //        _eventAccumulatorMock.Object,
        //        _dateTimeProviderMock.Object);

        //    var result = productService.IsBussinessDay();

        //    Assert.False(result);
        //}
    }
}
