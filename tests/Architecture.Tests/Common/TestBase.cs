using ImposterSyndrome.Application.Common.Interfaces;
using ImposterSyndrome.Domain.Common.Base;
using ImposterSyndrome.Infrastructure.Persistence;
using ImposterSyndrome.WebApi;
using System.Reflection;

namespace ImposterSyndrome.Architecture.UnitTests.Common;

public abstract class TestBase
{
    protected static readonly Assembly DomainAssembly = typeof(AggregateRoot<>).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(IApplicationDbContext).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(IWebApiMarker).Assembly;
}