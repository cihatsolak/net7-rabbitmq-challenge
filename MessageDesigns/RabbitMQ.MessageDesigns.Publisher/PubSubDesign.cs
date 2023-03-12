namespace RabbitMQ.MessageDesigns.Publisher
{
    internal static class PubSubDesign
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

            string exchangeName = "pub-sub-exchange";

            channel.ExchangeDeclare(
                exchange: exchangeName,
                type: ExchangeType.Fanout
                );

            byte[] message = Encoding.UTF8.GetBytes("Hello!");

            channel.BasicPublish(
                exchange: exchangeName,
                routingKey: string.Empty,
                body: message
                );

            Console.ReadKey();
        }
    }
}
