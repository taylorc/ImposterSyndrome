using ImposterSyndrome.Domain.Accessories;
using ImposterSyndrome.Domain.Heroes;
using ImposterSyndrome.Domain.Teams;
using Vogen;

namespace ImposterSyndrome.Infrastructure.Persistence.Configuration;

[EfCoreConverter<TeamId>]
[EfCoreConverter<HeroId>]
[EfCoreConverter<MissionId>]
[EfCoreConverter<AccessoryId>]
internal sealed partial class VogenEfCoreConverters;