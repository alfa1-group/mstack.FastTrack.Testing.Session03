using FastTrack.Testing.Session3.Application.Infrastructure;
using FastTrack.Testing.Session3.Application.Model;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastTrack.Testing.Session3.Application.Features;

[ApiController]
public class DeleteTaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteTaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete()]
    [Route("tasks/{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteTask.Command(id), cancellationToken);
        return response.Status switch
        {
            ResultStatus.NotFound => NotFound(),
            ResultStatus.BadRequest => BadRequest(),
            _ => Ok()
        };
    }
}

public static class DeleteTask
{
    public static void Add(IServiceCollection services)
    {
        services.AddScoped<IValidator<Command>, Validator>();
    }

    public record Command(Guid Id) : IRequest<Response>;

    public record Response(ResultStatus Status);

    public class Handler(TaskDbContext _dbContext, IValidator<Command> _validator) : IRequestHandler<Command, Response>
    {
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken = default)
        {
            if (!_validator.Validate(request).IsValid)
            {
                return new Response(ResultStatus.BadRequest);
            }

            await Delete(request.Id, cancellationToken);

            return new Response(ResultStatus.Ok);
        }

        public async Task<int> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(task => task.Id == id, cancellationToken).ConfigureAwait(false);
            if (task is null)
            {
                return 0;
            }

            _dbContext.Remove(task);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    internal class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
