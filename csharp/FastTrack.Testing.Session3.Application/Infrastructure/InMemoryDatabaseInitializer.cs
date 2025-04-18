using Microsoft.EntityFrameworkCore;

namespace FastTrack.Testing.Session3.Application.Infrastructure;

public static class InMemoryDatabaseInitializer
{
    public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services)
    {
        services.AddDbContext<TaskDbContext>(options =>
        {
            options.UseInMemoryDatabase(databaseName: "Tasks");
        });
        services.AddScoped<DataSeed>();

        return services;
    }
}

