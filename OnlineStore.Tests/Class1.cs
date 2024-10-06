using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using OnlineStore.MVC;
using OnlineStore.Contracts.Products;
using System.Net.Http.Json;



namespace OnlineStore.Tests
{
    public class Class1 : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public Class1(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetProducts_ReturnsOkResult_WithProducts()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Act
            var response = await _client.GetAsync("/api/products"); // Замените на нужный маршрут, если он отличается
            response.EnsureSuccessStatusCode();

            // Assert
            var products = await response.Content.ReadFromJsonAsync<ShortProductList>();

            Assert.NotNull(products);
            Assert.True(products.TotalCount > 0);
            Assert.Equal(1, products.PageNumber); // Проверка номера страницы, если это важно
        }
    }

}
