using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;

namespace WorkerService1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ProducerWorker>();
                    services.AddMassTransit(cfg =>
                    {
                       
                        cfg.AddConsumer<Consumer>();
                        cfg.UsingInMemory((context, config) =>
                        {
                            config.ReceiveEndpoint("Message-queue",cfg=>cfg.ConfigureConsumer<Consumer>(context));
                        });
                    });
                    services.AddMassTransitHostedService();
                });
    }
}
