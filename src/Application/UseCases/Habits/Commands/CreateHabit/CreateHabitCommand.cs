using ImposterSyndrome.Application.Common.Interfaces;

namespace ImposterSyndrome.Application.UseCases.Habits.Commands.CreateHabit;

public record CreateHabitCommand() : IRequest<ErrorOr<Success>>;

internal sealed class CreateHabitCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CreateHabitCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CreateHabitCommand request, CancellationToken cancellationToken)
    {
        // TODO: Add your business logic and persistence here

        throw new NotImplementedException();
    }
}

internal sealed class CreateHabitCommandValidator : AbstractValidator<CreateHabitCommand>
{
    public CreateHabitCommandValidator()
    {
        // TODO: Add your validation rules here.  For example: RuleFor(p => p.Foo).NotEmpty()
    }
}