using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.AppServices.Attributes.Repositories;
using OnlineStore.AppServices.Attributes.Services;
using OnlineStore.AppServices.Common.CacheService;
using OnlineStore.AppServices.Common.Redis;
using OnlineStore.DataAccess.Attributes.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Infrastructure.Mappings;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace OnlineStore.ComponentRegistrar
{
    /// <summary>
    /// Класс регистрации компонентов приложения.
    /// </summary>
    public static class OnlineStoreRegistrar
    {
        public static void AddComponents(IServiceCollection services, IConfiguration configuration)
        {
            RegisterRepositories(services, configuration);
            RegisterServices(services, configuration);
            RegisterMapper(services);
        }

        private static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DapperDbContext>(p =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new DapperDbContext(connectionString);
            });

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<OnlineStoreDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            services.AddScoped<IAttributesRepository, AttributesRepository>();
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisConfiguration = configuration
                .GetSection("Redis")
                .Get<RedisConfiguration>();

            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);

            services.AddScoped<IProductAttributeService, ProductAttributeService>();
            services.Decorate<IProductAttributeService, CachedProductAttributeService>();

            services.AddSingleton<IRedisCache, RedisCache>();
            services.AddSingleton<ICacheService, RedisCacheService>();
        }

        private static void RegisterMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductAttributeMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
