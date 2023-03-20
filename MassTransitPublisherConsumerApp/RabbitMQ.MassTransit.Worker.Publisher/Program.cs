IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<PublishMessageHostedService>(provider =>
        {
            using var scope = provider.CreateAsyncScope();
            IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

            return new PublishMessageHostedService(publishEndpoint);
        });

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
