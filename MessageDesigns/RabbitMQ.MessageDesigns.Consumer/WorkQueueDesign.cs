namespace RabbitMQ.MessageDesigns.Consumer
{
    internal static class WorkQueueDesign
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

            string queueName = "work-queue";

            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false
                );

            EventingBasicConsumer eventingBasicConsumer = new(channel);

            channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: eventingBasicConsumer
                );

            channel.BasicQos( //ölçeklendirme
                prefetchCount: 1, //her bir consumer 1 tane mesaj işleyecek
                prefetchSize: 0, //sınırsız mesaj alabilir
                global: false
                );

            eventingBasicConsumer.Received += (sender, args) =>
            {
                Console.WriteLine($"Message: {Encoding.UTF8.GetString(args.Body.Span)}");
            };

            Console.ReadKey();
        }
    }
}
