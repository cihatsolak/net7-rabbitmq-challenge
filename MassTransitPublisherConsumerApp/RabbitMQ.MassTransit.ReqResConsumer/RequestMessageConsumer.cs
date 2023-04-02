namespace RabbitMQ.MassTransit.ReqResConsumer
{
    public class RequestMessageConsumer : IConsumer<RequestMessage>
    {
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("İstek İşleniyor..");
            Console.ResetColor();

            await Task.Delay(TimeSpan.FromSeconds(3));

            await Console.Out.WriteLineAsync(context.Message.Text);

            await context.RespondAsync<ResponseMessage>(new()
            {
                Text = $"[Consumer]. response to request",
                Result = context.Message.Number1 + context.Message.Number2
            });
        }
    }
}
