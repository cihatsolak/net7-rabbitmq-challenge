namespace RabbitMQ.MessageDesigns.Publisher
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

            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false
                );

            byte[] message = Encoding.UTF8.GetBytes("Hello");

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: queueName,
                body: message
                );

            Console.ReadKey();
        }
    }
}
