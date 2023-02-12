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

// Kuyruk oluşturma
channel.QueueDeclare(
    queue: "test-queue",
    exclusive: false //true: sadece bağlantı kurulabilir. false: birden fazla bağlantı kurulabilir.
    );

//Kuyruktan mesaj okuma
EventingBasicConsumer eventingBasicConsumer = new(channel);

//Kanal'dan mesaj tüketme ayarları
channel.BasicConsume(
    queue: "test-queue",
    autoAck: false,
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

Console.ReadKey();