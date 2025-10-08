using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQListener.Api.Models;
using RabbitMQListener.Api.Services;
using System.Text;
using System.Text.Json;

namespace RabbitMQListener.Api.BackgroudServices;

public class StudentListenerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public StudentListenerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" }; // use localhost
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "student_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var student = JsonSerializer.Deserialize<Student>(message);

            if (student != null)
            {
                // Create scope here!
                using (var scope = _serviceProvider.CreateScope())
                {
                    var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();
                    await studentService.AddAsync(student);
                }
            }
        };

        await channel.BasicConsumeAsync(
            queue: "student_queue",
            autoAck: true,
            consumer: consumer
        );
    }
}
