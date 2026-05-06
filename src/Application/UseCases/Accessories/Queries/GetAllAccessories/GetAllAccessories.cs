using ImposterSyndrome.Application.Common.Interfaces;
using ImposterSyndrome.Application.UseCases.Habits.Queries.GetAllHabits;
using ImposterSyndrome.Domain.Accessories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImposterSyndrome.Application.UseCases.Accessories.Queries.GetAllAccessories;

public record GetAllAccessories : IRequest<IReadOnlyList<AccessoryDto>>;

public record AccessoryDto(Guid Id, string Name, string BarbellExercise);

internal sealed class GetAllAccessoriesHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetAllAccessories, IReadOnlyList<AccessoryDto>>
{
    public async Task<IReadOnlyList<AccessoryDto>> Handle(
       GetAllAccessories request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Accessories
            .Select(a => new AccessoryDto(
                a.Id.Value,
                a.Name,
                a.BarbellExercise.Value))
            .ToListAsync(cancellationToken);
    }
}
