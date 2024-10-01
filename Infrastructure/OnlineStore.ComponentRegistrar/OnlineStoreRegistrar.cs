﻿using AutoMapper;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.ApiClient;
using OnlineStore.AppServices.Attributes.Repositories;
using OnlineStore.AppServices.Attributes.Services;
using OnlineStore.AppServices.Authentication.Services;
using OnlineStore.AppServices.Common.CacheService;
using OnlineStore.AppServices.Common.Models;
using OnlineStore.AppServices.Common.Redis;
using OnlineStore.AppServices.Products.Repositories;
using OnlineStore.AppServices.Products.Services;
using OnlineStore.DataAccess.Attributes.Repositories;
using OnlineStore.DataAccess.Common;
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

            services.AddSingleton<IRedisCache, RedisCache>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddSingleton<IJwtGenerator, JwtGenerator>();

            services.Configure<DecoratorSettings>(configuration.GetSection("DecoratorSettings"));
            var decorationSettings = configuration.GetSection("DecoratorSettings").Get<DecoratorSettings>();
            if (decorationSettings?.EnableDecoration == true)
            {
                services.Decorate<IProductAttributeService, CachedProductAttributeService>();
            }
        }

        private static void RegisterMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductAttributeMappingProfile());
                mc.AddProfile(new ProductMappingProfile());
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
