using RabbitMQPublisher.Api.Models;

namespace RabbitMQPublisher.Api.Services;

public interface IStudentService
{
    Task AddAsync(Student student);
}