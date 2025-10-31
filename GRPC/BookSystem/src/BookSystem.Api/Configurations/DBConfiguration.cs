using BookSystem.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookGRPC.Server.Configurations;

public static class DBConfiguration
{
    public static IServiceCollection ConfigureDB(this IServiceCollection services, IConfiguration configuration)
    {

        var sqlConnectionString = configuration.GetConnectionString("DatabaseConnection");

        services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(sqlConnectionString));

        return services;
    }
}
