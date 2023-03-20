string rabbitMqUri = "amqp://localhost:5672";
string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri, host =>
    {
        host.Username("guest");
        host.Password("guest");
    });
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new Uri($"{rabbitMqUri}/{queueName}"));

Console.WriteLine("Message to Send: ");
string message = Console.ReadLine();

await sendEndpoint.Send<IMessage>(new ExampleMessage
{
    Text = $"Hello MassTransit! -- {message}"
});

Console.ReadKey();