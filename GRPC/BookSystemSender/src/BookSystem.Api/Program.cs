
using BookContracts;
using BookSystem.Api.Services;

namespace BookSystem.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // gRPC client configuration
        //builder.Services.AddGrpcClient<BookService.BookServiceClient>(o =>
        //{
        //    o.Address = new Uri("https://localhost:5001"); 
        //});
        
        builder.Services.AddGrpcClient<BookService.BookServiceClient>(o =>
        {
            o.Address = new Uri("https://localhost:7152");
        });


        builder.Services.AddScoped<IBookServiceGRPC, BookServiceGRPC>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
