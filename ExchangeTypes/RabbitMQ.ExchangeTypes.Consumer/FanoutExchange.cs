namespace RabbitMQ.ExchangeTypes.Consumer
{
    internal static class FanoutExchange
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
                queue: "fanout-exchange-queue",
                durable: true,
                exclusive: false,
                autoDelete: false
                );

            channel.QueueBind(
                queue: "fanout-exchange-queue",
                exchange: "fanout-exchange",
                routingKey: string.Empty
                );

            EventingBasicConsumer eventingBasicConsumer = new(channel);

            channel.BasicConsume(
               queue: "fanout-exchange-queue",
               autoAck: false,
               consumer: eventingBasicConsumer
                );

            IBasicProperties basicProperties = channel.CreateBasicProperties();
            basicProperties.ContentType = MediaTypeNames.Application.Json;
            basicProperties.DeliveryMode = 2; //Mesajın kalıcı hale getirilip getirilmeyeceğini belirleyen bir tamsayı değeri. 1 geçici, 2 kalıcıdır.

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
