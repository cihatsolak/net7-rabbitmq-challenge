IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<PublishMessageHostedService>();

        services.AddMassTransit(configure =>
        {
            configure.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host("amqp://localhost:5672");
            });
        });
    })
    .Build();

await host.RunAsync();
