namespace RabbitMQListener.Api.Models;

public class Student
{
    public long StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    override public string ToString()
    {
        return $"StudentId: {StudentId}, FirstName: {FirstName}, LastName: {LastName}, Age: {Age}, Email: {Email}";
    }
}
