using System;
using System.Threading;
using System.Threading.Tasks;
using GettingStarted.Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace GettingStarted
{
    public class PublishWorker : BackgroundService
    {
        private IBus bus;

        public PublishWorker(IBus bus)
        {
            this.bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                await bus.Send(new HelloMessage() {Id = NewId.NextGuid()}, stoppingToken);
                await bus.Publish(new HelloMessage()
                {
                    //Id = InVar.Id,
                    Id=NewId.NextGuid(),
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