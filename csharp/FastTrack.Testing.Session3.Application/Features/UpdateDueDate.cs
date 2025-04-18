using FastTrack.Testing.Session3.Application.Infrastructure;
using FastTrack.Testing.Session3.Application.Model;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastTrack.Testing.Session3.Application.Features;

[ApiController]
public class UpdateDueDateController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateDueDateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut()]
    [Route("tasks/{id}/duedate")]
    public async Task<IActionResult> Put(Guid id, [FromBody] DueDateDto dueDate, CancellationToken cancellationToken = default)
    {
        var command = new UpdateDueDate.Command()
        {
            Id = id,
            DueDate = dueDate.Value,
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

public record DueDateDto(DateTime? Value);

public static class UpdateDueDate
{
    public static void Add(IServiceCollection services)
    {
        services.AddScoped<IValidator<Command>, Validator>();
    }

    public record Command : IRequest<Response>
    {
        public required Guid Id { get; init; }
        public DateTime? DueDate { get; init; }
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

            task.DueDate = request.DueDate;
            _dbContext.Update(task);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new Response(ResultStatus.Ok);
        }
    }

    internal class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(q => q.Id).NotEmpty();
            RuleFor(command => command.DueDate)
                .Must(dueDate => !dueDate.HasValue || DateTime.Now.Date <= dueDate.Value)
                .WithMessage($"{nameof(Command.DueDate)} cannot be in the past.");
        }
    }
}
