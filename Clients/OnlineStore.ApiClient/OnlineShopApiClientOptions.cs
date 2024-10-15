using Microsoft.Extensions.Configuration;

namespace OnlineStore.ApiClient
{
    public sealed class OnlineShopApiClientOptions
    {
        [ConfigurationKeyName("BaseUrl")]
        public string BaseUrl { get; set; }
    }
}
