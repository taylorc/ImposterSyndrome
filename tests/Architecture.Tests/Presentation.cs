using ImposterSyndrome.Application.Common.Interfaces;
using ImposterSyndrome.Architecture.UnitTests.Common;
using ImposterSyndrome.Infrastructure.Persistence;

namespace ImposterSyndrome.Architecture.UnitTests;

public class Presentation : TestBase
{
    private static readonly Type IDbContext = typeof(IApplicationDbContext);
    private static readonly Type DbContext = typeof(ApplicationDbContext);

    [Fact]
    public void Endpoints_ShouldNotReferenceDbContext()
    {
        var types = Types
            .InAssembly(PresentationAssembly)
            .That()
            .HaveNameEndingWith("Endpoints");

        var result = types
            .ShouldNot()
            .HaveDependencyOnAny(DbContext.FullName, IDbContext.FullName)
            .GetResult();

        result.Should().BeSuccessful();
    }
}