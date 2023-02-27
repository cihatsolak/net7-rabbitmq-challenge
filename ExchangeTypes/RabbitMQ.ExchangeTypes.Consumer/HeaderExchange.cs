namespace RabbitMQ.ExchangeTypes.Consumer
{
    internal static class HeaderExchange
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

            channel.QueueDeclare(
                queue: "header-exchange-queue",
                durable: true,
                exclusive: false,
                autoDelete: false
                );

            channel.QueueBind(
                queue: "header-exchange-queue",
                exchange: "header-exchange",
                routingKey: string.Empty,
                arguments: new Dictionary<string, object>()
                           {
                               { "x-match", "all" }, //default: any geçerlidir
                               { "x-private-key", "7f98e2b5-1071-4516-a6f7-70f186e8a85e" }
                           }
                );

            EventingBasicConsumer eventingBasicConsumer = new(channel);

            channel.BasicConsume(
                queue: "header-exchange-queue",
                autoAck: false, //true: mesajların işlendiğinde otomatik sil. false: mesajları silmen için ben sana haber edeceğim.
                consumer: eventingBasicConsumer
                );

            eventingBasicConsumer.Received += (sender, args) =>
            {
                string message = Encoding.UTF8.GetString(args.Body.Span);
                Console.WriteLine("Message: {0}", message);

                channel.BasicAck(
                    deliveryTag: args.DeliveryTag,
                    multiple: false
                    );
            };
        }
    }
}
