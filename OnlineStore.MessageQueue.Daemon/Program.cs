using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineStore.ApiClient;
using OnlineStore.Infrastructure.JwtGenerator;
using OnlineStore.MessageQueue.Daemon.Consumers;

namespace OnlineStore.MessageQueue.Daemon
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddUserSecrets<Program>()
                .Build();

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<TestBusMessageConsumer>();
                        
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("rabbitmq://localhost", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });
                            cfg.ConfigureEndpoints(context, new DefaultEndpointNameFormatter(joinSeparator:".", prefix: "", includeNamespace: true));
                        });
                    });

                    services.Configure<MassTransitHostOptions>(options =>
                    {
                        options.WaitUntilStarted = true;
                        options.StartTimeout = TimeSpan.FromSeconds(1);
                        options.StartTimeout = TimeSpan.FromMinutes(1);
                    });

                    var baseUrl = config["OnlineShopApiClient:BaseUrl"];
                    services.AddHttpClient<IOnlineStoreApiClient, OnlineStoreApiClient>(client =>
                    {
                        client.BaseAddress = new Uri("baseUrl");
                    });
                    services.AddSingleton<IJwtGenerator, JwtGenerator>();
                })
                .Build();

            await host.RunAsync();
        }        
    }
}

 
