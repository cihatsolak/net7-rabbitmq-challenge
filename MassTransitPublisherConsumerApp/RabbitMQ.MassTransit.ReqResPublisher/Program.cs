Uri rabbitMqUri = new("amqp://localhost:5672");
string queueName = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri, host =>
    {
        host.Username("guest");
        host.Password("guest");
    });
});

await bus.StartAsync();

IRequestClient<RequestMessage> request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMqUri}/{queueName}"));

var responseMessage = await request.GetResponse<ResponseMessage>(new RequestMessage
{
    Text = $"Request Message",
    Number1 = 5,
    Number2 = 10,
});

Console.WriteLine($"Response Received: {responseMessage.Message.Text}, Result: {responseMessage.Message.Result}");

Console.ReadKey();