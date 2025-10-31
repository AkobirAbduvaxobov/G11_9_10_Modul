
namespace BookSystem.Api.Configurations;

public static class DIConfiguration
{
    public static IServiceCollection ConfigureDI(this IServiceCollection services)
    {
        //services.AddScoped<IBookRepository, BookRepository>();
        return services;
    }
}
