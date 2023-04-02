Uri rabbitMqUri = new("amqp://localhost:5672");
string queueName = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri, host =>
    {
        host.Username("guest");
        host.Password("guest");
    });

    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<RequestMessageConsumer>();
    });
});

await bus.StartAsync();

Console.ReadKey();