using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace ActiveMqConsumer;

public class MessageConsumerService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Run(() =>
        {
            var factory = new ConnectionFactory("tcp://localhost:61616");
            using var connection = factory.CreateConnection();
            connection.Start();
            using var session = connection.CreateSession();
            var destination = session.GetQueue("test-queue");
            using var consumer = session.CreateConsumer(destination);

            while (!stoppingToken.IsCancellationRequested)
            {
                var message = consumer.Receive() as ITextMessage;
                if (message != null)
                {
                    Console.WriteLine($"Received message: {message.Text}");
                }
            }
        }, stoppingToken);

        return Task.CompletedTask;
    }
}
