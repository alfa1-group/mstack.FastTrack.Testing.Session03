using FastTrack.Testing.Session3.Application.Model;
using Microsoft.EntityFrameworkCore;

namespace FastTrack.Testing.Session3.Application.Infrastructure;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {

    }

    public DbSet<TodoTask> Tasks { get; set; }
}

