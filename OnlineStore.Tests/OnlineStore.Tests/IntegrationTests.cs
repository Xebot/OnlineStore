using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using OnlineStore.Domain.Entities;
using OnlineStore.MVC;
using OnlineStore.DataAccess.Common;
using OnlineStore.Contracts.Products;
using System.Net.Http.Json;
using OnlineStore.Infrastructure.JwtGenerator;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using OnlineStore.AppServices.Common.DateTimeProviders;

public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public ProductsControllerTests(
        WebApplicationFactory<Program> factory)
    {

        //_client = factory.CreateClient();
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Регистрация конкретной реализации зависимости
                services.AddScoped<IJwtGenerator, JwtGenerator>();
            });
        });
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsOkResult_WithProducts()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        using var scope = _factory.Services.CreateScope();
        var jwtGenerator = scope.ServiceProvider.GetRequiredService<IJwtGenerator>();

        var token = jwtGenerator.GenerateToken();

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("https://localhost:7194/api/products"),
            Method = HttpMethod.Get,
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        // Assert
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.NotNull(products);
    }
}
