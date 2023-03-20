namespace RabbitMQ.MassTransit.Shared.Messages
{
    public interface IMessage
    {
        string Text { get; set; }
    }
}
