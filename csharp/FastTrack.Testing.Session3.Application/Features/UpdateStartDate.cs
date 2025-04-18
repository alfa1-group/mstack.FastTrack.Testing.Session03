using FastTrack.Testing.Session3.Application.Infrastructure;
using FastTrack.Testing.Session3.Application.Model;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastTrack.Testing.Session3.Application.Features;

[ApiController]
public class UpdateStartDateController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateStartDateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut()]
    [Route("tasks/{id}/startdate")]
    public async Task<IActionResult> Put(Guid id, [FromBody] StartDateDto startDate, CancellationToken cancellationToken = default)
    {
        var command = new UpdateStartDate.Command()
        {
            Id = id,
            StartDate = startDate.Value,
        };
        var response = await _mediator.Send(command, cancellationToken);
        return response.Status switch
        {
            ResultStatus.NotFound => NotFound(),
            ResultStatus.BadRequest => BadRequest(),
            _ => Ok()
        };
    }
}

public record StartDateDto(DateTime? Value);

public static class UpdateStartDate
{
    public static void Add(IServiceCollection services)
    {
        services.AddScoped<IValidator<Command>, Validator>();
    }

    public record Command : IRequest<Response>
    {
        public required Guid Id { get; init; }
        public DateTime? StartDate { get; init; }
    }

    public record Response(ResultStatus Status);

    public class Handler(TaskDbContext _dbContext, IValidator<Command> _validator) : IRequestHandler<Command, Response>
    {
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
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

            if (IsInvalidStartDateForTask(request.StartDate, task))
            {
                return new Response(ResultStatus.BadRequest);
            }

            task.StartDate = request.StartDate;
            _dbContext.Update(task);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new Response(ResultStatus.Ok);
        }

        private bool IsInvalidStartDateForTask(DateTime? startDate, TodoTask task)
        {
            return startDate.HasValue
                && task.DueDate.HasValue
                && startDate > task.DueDate;
        }
    }

    internal class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(q => q.Id).NotEmpty();
        }
    }
}
