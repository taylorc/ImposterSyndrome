using ImposterSyndrome.Application.Common.Interfaces;

namespace ImposterSyndrome.Application.UseCases.Habits.Commands.UpdateHabit;

public record UpdateHabitCommand() : IRequest<ErrorOr<Success>>;

internal sealed class UpdateHabitCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<UpdateHabitCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UpdateHabitCommand request, CancellationToken cancellationToken)
    {
        // TODO: Add your business logic and persistence here

        throw new NotImplementedException();
    }
}

internal sealed class UpdateHabitCommandValidator : AbstractValidator<UpdateHabitCommand>
{
    public UpdateHabitCommandValidator()
    {
        // TODO: Add your validation rules here.  For example: RuleFor(p => p.Foo).NotEmpty()
    }
}