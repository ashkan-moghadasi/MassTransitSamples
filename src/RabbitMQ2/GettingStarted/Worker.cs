using System;
using System.Threading;
using System.Threading.Tasks;
using GettingStarted.Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace GettingStarted
{
    public class Worker : BackgroundService
    {
        private IBus bus;

        public Worker(IBus bus)
        {
            this.bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await bus.Publish(new HelloMessage()
                {
                    Id = InVar.Id,
                    Name = "Hello World !"
                } 
                    //Set Unique Correlation Id
                    //,context => { context.CorrelationId = InVar.Id;}
                    ,stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}