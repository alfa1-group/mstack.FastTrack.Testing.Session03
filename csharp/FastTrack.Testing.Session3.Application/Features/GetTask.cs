using FastTrack.Testing.Session3.Application.Infrastructure;
using FastTrack.Testing.Session3.Application.Model;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastTrack.Testing.Session3.Application.Features;

[ApiController]
public class GetTaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetTaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    [Route("tasks/{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetTask.Query(id), cancellationToken);

        if (response.Status == ResultStatus.NotFound)
        {
            return NotFound();
        }

        if (response.Status == ResultStatus.BadRequest)
        {
            return BadRequest();
        }


        return Ok(response.Value);
    }
}

public static class GetTask
{
    public static void Add(IServiceCollection services)
    {
        services.AddTransient<IValidator<Query>, Validator>();
    }

    public record Query(Guid Id) : IRequest<Response> { }

    public record TaskDto()
    {
        public required string Summary { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public required ProgressStatus Status { get; set; }
        public required Priority Priority { get; set; }
        public required string Description { get; set; }
    }

    public record Response(ResultStatus Status)
    {
        public TaskDto Value { get; set; } = null!;
    }

    internal class Handler(TaskDbContext _dbContext, IValidator<Query> _validator) : IRequestHandler<Query, Response>
    {
        public async Task<Response> Handle(Query request, CancellationToken cancellationToken = default)
        {
            if (!_validator.Validate(request).IsValid)
            {
                return new Response(ResultStatus.BadRequest);
            }

            var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken).ConfigureAwait(false);
            if (task is null)
            {
                return new Response(ResultStatus.NotFound);
            }

            return new Response(ResultStatus.Ok) { Value = ToDto(task) };
        }

        private TaskDto ToDto(TodoTask task)
        {
            return new TaskDto()
            {
                Summary = task.Summary,
                DueDate = task.DueDate,
                StartDate = task.StartDate,
                Status = task.Status,
                Priority = task.Priority,
                Description = task.Description,
            };
        }
    }

    internal class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(q => q.Id).NotEmpty();
        }
    }
}
