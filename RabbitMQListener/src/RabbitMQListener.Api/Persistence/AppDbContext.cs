using Microsoft.EntityFrameworkCore;
using RabbitMQListener.Api.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RabbitMQListener.Api.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}