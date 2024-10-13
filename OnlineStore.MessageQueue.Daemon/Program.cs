using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransit.Transports;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineStore.ApiClient;
using OnlineStore.Infrastructure.JwtGenerator;
using System;
using System.Threading.Tasks;

namespace OnlineStore.MessageQueue.Daemon
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumers();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("rabbitmq://localhost", h =>
                            {
                                h.Username("guest"); // Логин
                                h.Password("guest"); // Пароль
                            });
                            cfg.ConfigureEndpoints(context, new DefaultEndpointNameFormatter(".", "", true));
                        });
                    });

                    services.Configure<MassTransitHostOptions>(options =>
                    {
                        options.WaitUntilStarted = true;
                        options.StartTimeout = TimeSpan.FromSeconds(30);
                        options.StopTimeout = TimeSpan.FromMinutes(1);
                    });
                    services.AddSingleton<IMessageProducer, MessageProducer>();

                    services.AddHttpClient<IOnlineStoreApiClient, OnlineStoreApiClient>(client =>
                    {
                        client.BaseAddress = new Uri("https://localhost:7194/api/");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                    });
                    services.AddSingleton<IJwtGenerator, JwtGenerator>();
                })
                .Build();

            var producer = host.Services.GetRequiredService<IMessageProducer>();
            await producer.PublishMessage("Hello, RabbitMQ!");
            await host.RunAsync();

            await host.RunAsync();
        }
    }
}
