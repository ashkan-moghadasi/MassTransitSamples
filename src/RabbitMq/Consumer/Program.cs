using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var busControl = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.ReceiveEndpoint("order-created-event", cfg=>cfg.Consumer<OrderCreatedConsumer>());
            });
            await busControl.StartAsync(new CancellationToken());

            try
            {
                Console.WriteLine("Press Enter To Exit");
                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            } 
            
        }
    }
}
