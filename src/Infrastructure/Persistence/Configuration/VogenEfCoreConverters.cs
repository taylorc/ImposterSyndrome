using ImposterSyndrome.Domain.Accessories;
using ImposterSyndrome.Domain.Heroes;
using ImposterSyndrome.Domain.Programme.Wendler531;
using ImposterSyndrome.Domain.Teams;
using Vogen;

namespace ImposterSyndrome.Infrastructure.Persistence.Configuration;

[EfCoreConverter<TeamId>]
[EfCoreConverter<HeroId>]
[EfCoreConverter<MissionId>]
[EfCoreConverter<AccessoryId>]
[EfCoreConverter<Wendler531ProgrammeId>]
internal sealed partial class VogenEfCoreConverters;