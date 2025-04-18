using FastTrack.Testing.Session3.Application.Infrastructure;
using FastTrack.Testing.Session3.Application.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastTrack.Testing.Session3.Application.Features;

[ApiController]
public class GetTasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetTasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    [Route("tasks")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetAllTasks.Query(), cancellationToken));
    }
}

public static class GetAllTasks
{
    public record Query : IRequest<IReadOnlyList<Response>>
    {
    }

    public record Response
    {
        public required Guid Id { get; init; }
        public required string Summary { get; init; }
        public DateTime? DueDate { get; init; }
        public required ProgressStatus Status { get; init; }
        public required Priority Priority { get; init; }
    }

    public class Handler(TaskDbContext _dbContext) : IRequestHandler<Query, IReadOnlyList<Response>>
    {
        public async Task<IReadOnlyList<Response>> Handle(Query _, CancellationToken cancellationToken = default)
        {
            var tasks = _dbContext.Tasks.AsAsyncEnumerable();

            var result = new List<Response>();
            await foreach (var task in tasks.WithCancellation(cancellationToken).ConfigureAwait(false))
            {
                result.Add(ToResponse(task));
            }
            return result;
        }

        private Response ToResponse(TodoTask task)
        {
            return new Response()
            {
                Id = task.Id,
                Summary = task.Summary,
                DueDate = task.DueDate,
                Status = task.Status,
                Priority = task.Priority,
            };
        }
    }
}
