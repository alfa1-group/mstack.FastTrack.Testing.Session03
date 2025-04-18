namespace FastTrack.Testing.Session3.Application.Model;

public record TodoTask
{
    public required Guid Id { get; set; }

    public required string Summary { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? StartDate { get; set; }

    public ProgressStatus Status { get; set; }

    public Priority Priority { get; set; }

    public required string Description { get; set; }
}
