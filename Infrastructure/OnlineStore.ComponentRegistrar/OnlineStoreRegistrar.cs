using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.AppServices.Attributes.Repositories;
using OnlineStore.AppServices.Attributes.Services;
using OnlineStore.AppServices.Common.Redis;
using OnlineStore.DataAccess.Attributes.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Infrastructure.Mappings;

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
            RegisterServices(services);
            RegisterMapper(services);
        }

        private static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbContext>(p =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new DbContext(connectionString);
            });

            services.AddScoped<IAttributesRepository, AttributesRepository>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IProductAttributeService, ProductAttributeService>();

            services.AddScoped<IRedisCache, RedisCache>();
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
