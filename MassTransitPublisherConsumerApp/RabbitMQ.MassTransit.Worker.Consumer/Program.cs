IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configure =>
        {
            configure.AddConsumer<ExampleMessageConsumer>();

            configure.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host("amqp://localhost:5672");

                configurator.ReceiveEndpoint("example-message-queue", endpoint =>
                {
                    endpoint.ConfigureConsumer<ExampleMessageConsumer>(context);
                });
            });
        });
    })
    .Build();

await host.RunAsync();
