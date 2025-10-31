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
        var factory = new ConnectionFactory()
        {
            HostName = "localhost", // host.docker.internal Docker ichidan Windows RabbitMQ ga ulanish localhost orqali amalga oshiriladi
            //Port = 5672,
            UserName = "guest",
            Password = "guest"
        };

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
            var path = @"D:\log.txt";
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var student = JsonSerializer.Deserialize<Student>(message);

                if (student != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();
                    File.AppendAllText(path, $"Info : {DateTime.Now}: {student.ToString}{Environment.NewLine}");
                    await studentService.AddAsync(student);
                }
                File.AppendAllText(path, $"Obj is null : {DateTime.Now}: {Environment.NewLine}");
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, $"Error : {DateTime.Now}: {ex.Message}{Environment.NewLine}");
            };

            await channel.BasicConsumeAsync(
                queue: "student_queue",
                autoAck: true,
                consumer: consumer
            );

            await Task.Delay(Timeout.Infinite, stoppingToken);
        };
    }
}
