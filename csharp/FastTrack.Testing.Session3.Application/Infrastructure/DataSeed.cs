namespace FastTrack.Testing.Session3.Application.Infrastructure;

public class DataSeed
{
    private readonly TaskDbContext _dbContext;

    public DataSeed(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SeedData()
    {
        if (!_dbContext.Tasks.Any())
        {
            _dbContext.Tasks.AddRange(
                new Model.TodoTask
                {
                    Id = Guid.NewGuid(),
                    Summary = "My First Task",
                    Description = "Description 1",
                },
                new Model.TodoTask
                {
                    Id = Guid.NewGuid(),
                    Summary = "My Second Task",
                    Description = "Description 2",
                });

            _dbContext.SaveChanges();
        }
    }
}

