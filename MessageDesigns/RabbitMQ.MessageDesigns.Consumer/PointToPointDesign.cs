namespace RabbitMQ.MessageDesigns.Consumer
{
    internal static class PointToPointDesign
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

            string queueName = "p2p-queue";

            EventingBasicConsumer eventingBasicConsumer = new(channel);

            channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: eventingBasicConsumer
                );

            eventingBasicConsumer.Received += (sender, args) =>
            {
                Console.WriteLine($"Message: {Encoding.UTF8.GetString(args.Body.Span)}");
            };

            Console.ReadKey();
        }
    }
}
