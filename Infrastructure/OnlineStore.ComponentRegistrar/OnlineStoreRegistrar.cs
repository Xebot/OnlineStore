using AutoMapper;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.ApiClient;
using OnlineStore.AppServices.Attributes.Repositories;
using OnlineStore.AppServices.Attributes.Services;
using OnlineStore.AppServices.Authentication.Services;
using OnlineStore.AppServices.Carts.Repositories;
using OnlineStore.AppServices.Carts.Services;
using OnlineStore.AppServices.Categories.Repositories;
using OnlineStore.AppServices.Categories.Services;
using OnlineStore.AppServices.Common.CacheService;
using OnlineStore.AppServices.Common.DateTimeProviders;
using OnlineStore.AppServices.Common.Events.Common;
using OnlineStore.AppServices.Common.Events.Handlers;
using OnlineStore.AppServices.Common.Models;
using OnlineStore.AppServices.Common.NotificationServices;
using OnlineStore.AppServices.Common.Redis;
using OnlineStore.AppServices.Images.Repositories;
using OnlineStore.AppServices.Images.Services;
using OnlineStore.AppServices.Products.Repositories;
using OnlineStore.AppServices.Products.Services;
using OnlineStore.DataAccess.Attributes.Repositories;
using OnlineStore.DataAccess.Carts.Repositories;
using OnlineStore.DataAccess.Categories.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.DataAccess.Events;
using OnlineStore.DataAccess.Images.Repositories;
using OnlineStore.DataAccess.MIddlewares;
using OnlineStore.DataAccess.Products.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.JwtGenerator;
using OnlineStore.Infrastructure.Mappings;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using System.Text;

namespace OnlineStore.ComponentRegistrar
{
    /// <summary>
    /// Класс регистрации компонентов приложения.
    /// </summary>
    public static class OnlineStoreRegistrar
    {
        public static void AddComponents(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<MutableOnlineStoreDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "Jwt:Issuer",
                        ValidAudience = "Jwt:Audience",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("In that case the Microsoft.IdentityModel.JsonWebTokens library throws an exception similar to the one you describe "))
                    };
                });

            RegisterRepositories(services, configuration);
            RegisterServices(services, configuration);
            RegisterMapper(services);
            RegisterApiClients(services);
            RegisterScheduler(services, configuration);
        }

        public static void RegisterMiddlewares(WebApplication app)
        {
            app.UseMiddleware<TransactionMiddleware>();
        }

        private static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MutableOnlineStoreDbContext>(options =>
                options.UseNpgsql(connectionString)
            );
            services.AddDbContext<ReadonlyOnlineStoreDbContext>(options =>
                options.UseNpgsql(connectionString)
            );            

            services.AddScoped<IAttributesRepository, AttributesRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisConfiguration = configuration
                .GetSection("Redis")
                .Get<RedisConfiguration>();

            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);

            services.AddScoped<IProductAttributeService, ProductAttributeService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ICartService, CartService>();

            services.AddSingleton<IRedisCache, RedisCache>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddSingleton<IJwtGenerator, JwtGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IEventDispatcher, EventDispatcher>();
            services.AddScoped<IEventAccumulator, EventAccumulator>();

            #region Notifications
            services.AddScoped<INotificationStrategy, EmailNotificationStrategy>();
            services.AddScoped<INotificationStrategy, TelegramNotificationStrategy>();

            services.AddScoped<INotificationService, NotificationService>();
            #endregion

            services.Configure<DecoratorSettings>(configuration.GetSection("DecoratorSettings"));
            var decorationSettings = configuration.GetSection("DecoratorSettings").Get<DecoratorSettings>();
            if (decorationSettings?.EnableDecoration == true)
            {
                services.Decorate<IProductAttributeService, CachedProductAttributeService>();
                services.Decorate<ICartService, CachedCartService>();
            }

            services.Scan(scan => 
                scan.FromAssemblyOf<AddProductEventHandler>()
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
        }

        private static void RegisterMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductAttributeMappingProfile());
                mc.AddProfile(new ProductMappingProfile());
                mc.AddProfile(new CategoryMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void RegisterApiClients(IServiceCollection services)
        {
            services.AddHttpClient<IOnlineStoreApiClient, OnlineStoreApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7194/api/");
            });
        }

        private static void RegisterScheduler(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(conf =>
                conf.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(options =>
                {
                    options.UseNpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
                })
            );

            services.AddHangfireServer();
        }
    }
}
