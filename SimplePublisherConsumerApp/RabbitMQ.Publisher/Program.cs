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

//Kuyruğa mesaj gönderme
byte[] message = Encoding.UTF8.GetBytes("Hello!");

channel.BasicPublish(
    exchange: string.Empty, //exchange belirtmediğim için rabbitmq direct exchange olarak algılar.
    routingKey: "test-queue", //default exchange kullandığım için routingkey'e queue adı tanımlanır.
    body: message
    );

Console.ReadKey();