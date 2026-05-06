using Microsoft.EntityFrameworkCore;
using ImposterSyndrome.Application.Common.Interfaces;

namespace ImposterSyndrome.Application.UseCases.Habits.Queries.GetAllHabits;

public record GetAllHabitsQuery : IRequest<ErrorOr<HabitDto>>;

public record HabitDto(/* Add properties here */);

internal sealed class GetAllHabitsQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetAllHabitsQuery, ErrorOr<HabitDto>>
{
    public async Task<ErrorOr<HabitDto>> Handle(
        GetAllHabitsQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}