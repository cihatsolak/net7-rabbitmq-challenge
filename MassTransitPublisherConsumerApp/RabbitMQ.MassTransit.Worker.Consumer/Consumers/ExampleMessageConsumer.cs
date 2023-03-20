namespace RabbitMQ.MassTransit.Worker.Consumer.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        private readonly ILogger<ExampleMessageConsumer> _logger;

        public ExampleMessageConsumer(ILogger<ExampleMessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<IMessage> context)
        {
            _logger.LogInformation("Incoming Message: {@message}", context.Message.Text);

            return Task.CompletedTask;
        }
    }
}
