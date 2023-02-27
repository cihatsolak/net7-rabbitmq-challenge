namespace RabbitMQ.ExchangeTypes.Publisher
{
    internal static class HeaderExchange
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

            channel.ExchangeDeclare(
                exchange: "header-exchange",
                type: ExchangeType.Headers,
                durable: true,
                autoDelete: false
                );

            IBasicProperties basicProperties = channel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>()
            {
                { "x-private-key", "7f98e2b5-1071-4516-a6f7-70f186e8a85e" }
            };

            channel.BasicPublish(
                exchange: "header-exchange",
                routingKey: string.Empty,
                body: Encoding.UTF8.GetBytes("Hello Header Exchange!"),
                basicProperties: basicProperties
                );


            Console.ReadKey();
        }
    }
}
