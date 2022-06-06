using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EventBus.Messages;
using MassTransit;
using Newtonsoft.Json;

namespace Consumer
{
    public class OrderCreatedConsumer : IConsumer<IOrderCreated>
    {
        public  Task Consume(ConsumeContext<IOrderCreated> context)
        {
            var jsonObject = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"Order Created:  {jsonObject}");
            return Task.CompletedTask;
        }
    }
}