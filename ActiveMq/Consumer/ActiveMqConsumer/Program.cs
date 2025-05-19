using ActiveMqConsumer;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<MessageConsumerService>();

var host = builder.Build();
host.Run();
