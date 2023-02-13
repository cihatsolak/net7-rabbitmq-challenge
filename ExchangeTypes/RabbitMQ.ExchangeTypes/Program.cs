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

// Exchange Oluşturma
channel.ExchangeDeclare(
    exchange: "direct-exchange-example",
    type: ExchangeType.Direct,
    durable: true,
    autoDelete: false
    );

channel.BasicPublish(
    exchange: "direct-exchange-example",
    routingKey: "bank",
    body: Encoding.UTF8.GetBytes("Hello")
    );

Console.ReadKey();