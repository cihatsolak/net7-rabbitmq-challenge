namespace RabbitMQ.ExchangeTypes.Consumer
{
    internal static class DirectExchange
    {
        internal static void Run()
        {
            //Bağlantı oluşturma
            IConnectionFactory connectionFactory = new ConnectionFactory()
            {
                Port = 5672,
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            //Bağlantı aktifleştirme ve kanal açma
            using IConnection connection = connectionFactory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "direct-exchange-queue",
                durable: true,
                exclusive: false,
                autoDelete: false
                );

            channel.QueueBind(
                queue: "direct-exchange-queue",
                exchange: "direct-exchange-example",
                routingKey: "bank"
                );

            EventingBasicConsumer eventingBasicConsumer = new(channel);

            channel.BasicConsume(
                queue: "direct-exchange-queue",
                autoAck: true,
                consumer: eventingBasicConsumer
                );

            eventingBasicConsumer.Received += (sender, args) =>
            {
                string message = Encoding.UTF8.GetString(args.Body.Span);
                Console.WriteLine($"Message: {message}");
            };
        }
    }
}
