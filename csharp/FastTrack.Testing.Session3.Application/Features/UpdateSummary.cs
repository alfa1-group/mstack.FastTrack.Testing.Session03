using FastTrack.Testing.Session3.Application.Infrastructure;
using FastTrack.Testing.Session3.Application.Model;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastTrack.Testing.Session3.Application.Features;

[ApiController]
public class UpdateSummaryController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateSummaryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut()]
    [Route("tasks/{id}/summary")]
    public async Task<IActionResult> Put(Guid id, [FromBody] SummaryDto summary, CancellationToken cancellationToken = default)
    {
        var command = new UpdateSummary.Command()
        {
            Id = id,
            Summary = summary.Value,
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

public record SummaryDto(string Value);

public static class UpdateSummary
{
    public static void Add(IServiceCollection services)
    {
        services.AddScoped<IValidator<Command>, Validator>();
    }

    public record Command : IRequest<Response>
    {
        public required Guid Id { get; init; }
        public required string Summary { get; init; }
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

            task.Summary = request.Summary;
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
            RuleFor(command => command.Summary)
                .NotEmpty()
                .WithMessage($"{nameof(Command.Summary)} must contain at least one visible character.");
        }
    }
}
