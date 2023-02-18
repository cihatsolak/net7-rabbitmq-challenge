namespace RabbitMQ.ExchangeTypes.Consumer
{
    internal static class FanoutExchange
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
                exchange: "fanout-exchange",
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false
                );

            channel.BasicPublish(
                exchange: "fanout-exchange",
                routingKey: string.Empty,
                body: Encoding.UTF8.GetBytes("Hello Fanout!")
                );

        }
    }
}
