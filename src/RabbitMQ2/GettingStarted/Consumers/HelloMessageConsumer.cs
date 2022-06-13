using Microsoft.Extensions.Logging;

namespace GettingStarted.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;

    public class HelloMessageConsumer :
        IConsumer<HelloMessage>
    {
        private readonly ILogger<HelloMessageConsumer> _logger;

        public HelloMessageConsumer(ILogger<HelloMessageConsumer> logger)
        {
            _logger = logger;
        }

        public  Task Consume(ConsumeContext<HelloMessage> context)
        {
            _logger.LogInformation($"ID={context.Message.Id}  Message={context.Message.Name} MessageId={context.MessageId} Correlation={context.CorrelationId} ConversationId={context.ConversationId}");
            
            return Task.CompletedTask;
        }
    }
}