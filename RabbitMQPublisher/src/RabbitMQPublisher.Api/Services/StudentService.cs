using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQPublisher.Api.Configurations.Settings;
using RabbitMQPublisher.Api.Models;

namespace RabbitMQPublisher.Api.Services;

public class StudentService : IStudentService
{
    private readonly RabbitMQSettings _rabbitMQSettings;

    public StudentService(RabbitMQSettings rabbitMQSettings)
    {
        _rabbitMQSettings = rabbitMQSettings;
    }

    public async Task AddAsync(Student student)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMQSettings.HostName,
            UserName = _rabbitMQSettings.UserName,
            Password = _rabbitMQSettings.Password
        };

        using IConnection connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _rabbitMQSettings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var message = JsonSerializer.Serialize(student);
        var body = Encoding.UTF8.GetBytes(message);

        var basicProperties = new BasicProperties();
        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: _rabbitMQSettings.QueueName,
            basicProperties: basicProperties,
            mandatory: false,
            body: body
        );


        await Task.CompletedTask;
    }
}
