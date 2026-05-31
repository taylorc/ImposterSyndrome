namespace ImposterSyndrome.Domain.Programme.Wendler531;

public sealed class Wendler531ProgrammeSpec : SingleResultSpecification<Wendler531Programme>
{
    public static Wendler531ProgrammeSpec ById(Wendler531ProgrammeId programmeId)
    {
        var spec = new Wendler531ProgrammeSpec();
        spec.Query.Where(p => p.Id == programmeId);
        return spec;
    }
}
