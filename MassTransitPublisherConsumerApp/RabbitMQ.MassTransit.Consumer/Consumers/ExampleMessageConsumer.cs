namespace RabbitMQ.MassTransit.Consumer.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Incoming Message: {context.Message.Text}");

            return Task.CompletedTask;
        }
    }
}
