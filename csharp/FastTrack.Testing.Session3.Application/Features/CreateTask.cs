using FastTrack.Testing.Session3.Application.Infrastructure;
using FastTrack.Testing.Session3.Application.Model;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastTrack.Testing.Session3.Application.Features;

[ApiController]
public class CreateTaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateTaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost()]
    [Route("tasks")]
    public async Task<IActionResult> Post([FromBody] CreateTask.Command command, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }
}

public static class CreateTask
{
    public static void Add(IServiceCollection services)
    {
        services.AddScoped<IValidator<Command>, Validator>();
    }

    public record Command : IRequest<Response>
    {
        public required string Summary { get; init; }
        public DateTime? DueDate { get; init; }
        public DateTime? StartDate { get; set; }
    }

    public record Response(Guid Id);

    public class Handler(TaskDbContext _dbContext, IValidator<Command> _validator)
        : IRequestHandler<Command, Response>
    {
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken = default)
        {
            _validator.ValidateAndThrow(request);

            var task = ToModel(request);
            await Add(task, cancellationToken).ConfigureAwait(false);
            return new Response(task.Id);
        }

        private async Task<int> Add(TodoTask task, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(task, cancellationToken).ConfigureAwait(false);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private TodoTask ToModel(Command source)
        {
            return new TodoTask
            {
                Id = Guid.NewGuid(),
                Summary = source.Summary,
                DueDate = source.DueDate,
                StartDate = source.StartDate,
                Description = string.Empty,
            };
        }
    }

    internal class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(command => command.Summary)
                .NotEmpty()
                .WithMessage($"{nameof(Command.Summary)} must contain at least one visible character.");
            RuleFor(command => command.DueDate)
                .Must(dueDate => !dueDate.HasValue || MustBeTodayOrInTheFuture(dueDate.Value))
                .WithMessage($"{nameof(Command.DueDate)} cannot be in the past.");
            RuleFor(command => command.StartDate)
                .Must(startDate => !startDate.HasValue || MustBeTodayOrInTheFuture(startDate.Value))
                .WithMessage($"{nameof(Command.StartDate)} cannot be in the past.");
            RuleFor(command => command)
                .Must(command => StartDateMustBeBeforeDueDateWhenNotEmpty(command.StartDate, command.DueDate))
                .WithMessage($"{nameof(Command.StartDate)} must be smaller or equal to {nameof(Command.DueDate)}");
        }

        private static bool StartDateMustBeBeforeDueDateWhenNotEmpty(DateTime? startDate, DateTime? dueDate)
        {
            return !startDate.HasValue
                || !dueDate.HasValue
                || startDate.Value <= dueDate.Value;
        }

        private bool MustBeTodayOrInTheFuture(DateTime date)
        {
            return DateTime.Now.Date <= date.Date;
        }
    }
}
