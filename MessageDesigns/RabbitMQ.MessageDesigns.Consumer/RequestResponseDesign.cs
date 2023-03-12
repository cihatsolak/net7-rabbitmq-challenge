namespace RabbitMQ.MessageDesigns.Consumer
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

            EventingBasicConsumer eventingBasicConsumer = new(channel);

            channel.BasicConsume(
                queue: requestQueueName,
                autoAck: true,
                consumer: eventingBasicConsumer
                );

            eventingBasicConsumer.Received += (sender, args) =>
            {
                string message = Encoding.UTF8.GetString(args.Body.Span);
                Console.WriteLine($"Incoming Message: {message}");

                byte[] responseMessage = Encoding.UTF8.GetBytes($"Process completed. Message: {message}");

                IBasicProperties basicProperties = channel.CreateBasicProperties();
                basicProperties.CorrelationId = args.BasicProperties.CorrelationId;

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: args.BasicProperties.ReplyTo,
                    basicProperties: basicProperties,
                    body: responseMessage
                    );
            };

            Console.ReadKey();


        }
    }
}
