namespace GettingStarted.Consumers
{
    using MassTransit;

    /// <summary>
    /// در این کلاس می توان تنظیمات مربوط به
    /// Consumer or Endpoint
    /// رابه صورت دستی انجام داد
    /// </summary>
    public class HelloMessageConsumerDefinition :
        ConsumerDefinition<HelloMessageConsumer>
    {
        public HelloMessageConsumerDefinition()
        {
            // override the default endpoint name
            EndpointName = "HelloMessage-Queue";

            // limit the number of messages consumed concurrently
            // this applies to the consumer only, not the endpoint
            ConcurrentMessageLimit = 8;

            /*Endpoint(cfg=>
            {
                cfg.Name = "HelloMessage-Queue";
                // set if each service instance should have its own endpoint for the consumer
                // so that messages fan out to each instance.
                cfg.InstanceId = "SomethingUnique";
            });*/
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<HelloMessageConsumer> consumerConfigurator)
        {
            
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
            
        }
    }
}