using Microsoft.Extensions.Configuration;
using RabbitMQPublisher.Api.Services;
using System;

namespace RabbitMQPublisher.Api.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDI(this IServiceCollection services, IConfiguration configuration)
    {

        var rabbitMqSection = configuration.GetSection("RabbitMQ");
        var hostName = rabbitMqSection["HostName"];
        var userName = rabbitMqSection["UserName"];
        var password = rabbitMqSection["Password"];
        var queueName = rabbitMqSection["QueueName"];

        var rabbitMQSettings = new Settings.RabbitMQSettings
        {
            HostName = hostName,
            UserName = userName,
            Password = password,
            QueueName = queueName
        };

        services.AddSingleton(rabbitMQSettings);

        services.AddScoped<IStudentService, StudentService>();
        return services;
    }
}
