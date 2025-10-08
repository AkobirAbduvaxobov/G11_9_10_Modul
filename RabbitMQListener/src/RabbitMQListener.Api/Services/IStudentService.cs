using RabbitMQListener.Api.Models;

namespace RabbitMQListener.Api.Services;

public interface IStudentService
{
    Task AddAsync(Student student);
}