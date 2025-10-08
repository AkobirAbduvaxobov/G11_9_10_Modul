using RabbitMQListener.Api.Models;
using RabbitMQListener.Api.Persistence;

namespace RabbitMQListener.Api.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }
}
