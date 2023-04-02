namespace RabbitMQ.MassTransit.Shared.RequestResponseMessages
{
    public record RequestMessage
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }

        public string Text { get; set; }
    }
}
