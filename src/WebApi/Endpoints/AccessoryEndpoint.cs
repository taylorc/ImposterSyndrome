using ImposterSyndrome.Application.UseCases.Accessories.Queries.GetAllAccessories;
using ImposterSyndrome.Domain.Accessories;
using ImposterSyndrome.WebApi.Extensions;
using MediatR;

namespace ImposterSyndrome.WebApi.Endpoints;

public static class AccessoryEndpoint
{
    public static void MapAccessoryEndpoints(this WebApplication app)
    {
        var group = app.MapApiGroup("accessories");

        group
            .MapGet("/", async (ISender sender, CancellationToken ct) =>
            {
                var results = await sender.Send(new GetAllAccessories(), ct);
                return TypedResults.Ok(results);
            })
            .WithName("GetAllAccessories")
            .ProducesGet<Accessory[]>();
    }
}
