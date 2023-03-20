namespace RabbitMQ.MassTransit.Worker.Publisher
{
    public class PublishMessageHostedService : BackgroundService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishMessageHostedService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int index = 0;

            while (true)
            {
                ExampleMessage exampleMessage = new()
                {
                    Text = $"{++index}. Message"
                };

                await _publishEndpoint.Publish<IMessage>(exampleMessage, stoppingToken);

                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}