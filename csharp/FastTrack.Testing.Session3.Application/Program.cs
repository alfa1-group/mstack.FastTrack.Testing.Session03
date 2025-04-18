using FastTrack.Testing.Session3.Application.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInMemoryDatabase();

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    // This is necessary, because Swagger would otherwise trip over the duplicate
    // 'Response' name for the return objects with each feature.  
    config.CustomSchemaIds(x => (x.FullName ?? x.Name)
                                .Replace(x.Namespace + ".", string.Empty)// to remove the full namespace from the schema
                                .Replace('+', '.')); // Swagger schemas cannot handle + in the name.
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    // Seed data base
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeed>();
    seeder.SeedData();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
