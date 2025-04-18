using FastTrack.Testing.Session3.Application.Features;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        //services.AddValidatorsFromAssemblyContaining<Program>();

        // Add Features
        CreateTask.Add(services);
        GetTask.Add(services);
        DeleteTask.Add(services);
        UpdateSummary.Add(services);
        UpdateDueDate.Add(services);
        UpdateStartDate.Add(services);
        UpdateStatus.Add(services);
        UpdatePriority.Add(services);
        UpdateDescription.Add(services);

        return services;
    }
}