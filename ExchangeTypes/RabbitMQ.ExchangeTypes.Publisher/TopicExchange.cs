namespace RabbitMQ.ExchangeTypes.Publisher
{
    internal static class TopicExchange
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
                exchange: "topic-exchange",
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false
                );

            channel.BasicPublish(
                exchange: "topic-exchange",
                routingKey: "Saglik.Yiyecek.*",
                body: Encoding.UTF8.GetBytes("Hello Fanout!")
                );

            Console.ReadKey();
        }
    }
}
