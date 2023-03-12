namespace RabbitMQ.MessageDesigns.Publisher
{
    internal static class RequestResponseDesign
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

            string requestQueueName = "request-response-queue";

            channel.QueueDeclare(
                queue: requestQueueName,
                durable: false,
                exclusive: false,
                autoDelete: false
                );

            string replyQueueName = channel.QueueDeclare().QueueName;
            string correlationId = Guid.NewGuid().ToString();

            IBasicProperties basicProperties = channel.CreateBasicProperties();
            basicProperties.CorrelationId = correlationId;
            basicProperties.ReplyTo = replyQueueName;

            for (int index = 0; index < 100; index++)
            {
                byte[] message = Encoding.UTF8.GetBytes($"Hello {index}");

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: requestQueueName,
                    basicProperties: basicProperties,
                    body: message
                    );
            }

            EventingBasicConsumer eventingBasicConsumer = new(channel);

            channel.BasicConsume(
                queue: replyQueueName,
                autoAck: true,
                consumer: eventingBasicConsumer
                );

            eventingBasicConsumer.Received += (sender, args) =>
            {
                if (args.BasicProperties.CorrelationId == correlationId)
                {
                    Console.WriteLine($"Response: {Encoding.UTF8.GetString(args.Body.Span)}");
                }
            };

            Console.ReadKey();
        }
    }
}
