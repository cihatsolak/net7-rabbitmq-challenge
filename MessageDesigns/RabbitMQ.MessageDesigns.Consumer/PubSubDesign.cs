namespace RabbitMQ.MessageDesigns.Consumer
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
            string randomQueueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(
                queue: randomQueueName,
                exchange: exchangeName,
                routingKey: string.Empty
                );

            EventingBasicConsumer eventingBasicConsumer = new(channel);

            channel.BasicQos(
                prefetchCount: 1,
                prefetchSize: 0,
                global: false
                );

            channel.BasicConsume(
                queue: randomQueueName,
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
