using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQPublisher.Api.Models;
using RabbitMQPublisher.Api.Services;

namespace RabbitMQPublisher.Api.Controllers;

[Route("api/students")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    public StudentsController(Services.IStudentService studentService)
    {
        _studentService = studentService;
    }
    [HttpPost]
    public async Task CreateStudent([FromBody] Student student)
    {
        await _studentService.AddAsync(student);
    }
}
