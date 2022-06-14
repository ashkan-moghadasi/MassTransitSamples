using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GettingStarted.Contracts;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace GettingStarted
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        //نام گذاری پیش فرض به صورت پاسکال کیس و بر اساس نام کلاس مصرف کننده است
                        //x.SetKebabCaseEndpointNameFormatter();

                        // By default, sagas are in-memory, but should be changed to a durable
                        // saga repository.
                        x.SetInMemorySagaRepositoryProvider();

                        var entryAssembly = Assembly.GetEntryAssembly();

                        x.AddConsumers(entryAssembly);
                        x.AddSagaStateMachines(entryAssembly);
                        x.AddSagas(entryAssembly);
                        x.AddActivities(entryAssembly);
                        x.UsingRabbitMq((context, configurator) =>
                        {
                            configurator.Host("localhost","/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });
                            configurator.ConfigureEndpoints(context);
                            //Fill CorrelationId with HelloMessage.Id
                            configurator.SendTopology.UseCorrelationId<HelloMessage>(m=>m.Id);
                        });
                        /*x.UsingInMemory((context, cfg) =>
                        {
                            cfg.ConfigureEndpoints(context);
                        });*/
                        x.AddHostedService<PublishWorker>();
                    });
                });
    }
}
