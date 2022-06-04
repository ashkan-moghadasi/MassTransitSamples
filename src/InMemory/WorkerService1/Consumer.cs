using System.Threading.Tasks;

using MassTransit;
using Microsoft.Extensions.Logging;

namespace WorkerService1
{
    public class Consumer : IConsumer<Message>
    {
        private readonly ILogger<Consumer> _logger;

        public Consumer(ILogger<Consumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Message> context)
        {
            _logger.LogInformation($"Received Text :{context.Message.Value}");
            return Task.CompletedTask;
        }
    }
}