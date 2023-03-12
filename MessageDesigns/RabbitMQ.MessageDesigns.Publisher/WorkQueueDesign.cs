namespace RabbitMQ.MessageDesigns.Publisher
{
    internal static class WorkQueueDesign
    {
        internal static async Task Run()
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


            for (int index = 0; index < 150; index++)
            {
                await Task.Delay(400);

                byte[] message = Encoding.UTF8.GetBytes($"Hello {index}");

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: queueName,
                    body: message
                    );
            }

            Console.ReadKey();
        }
    }
}
