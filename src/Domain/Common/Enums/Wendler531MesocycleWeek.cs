using Ardalis.SmartEnum;

namespace ImposterSyndrome.Domain.Common.Enums;

/// <summary>
/// One week within the classic four-week Wendler 5/3/1 mesocycle (intensity wave plus deload).
/// </summary>
public sealed class Wendler531MesocycleWeek : SmartEnum<Wendler531MesocycleWeek, int>
{
    private Wendler531MesocycleWeek(string name, int value)
        : base(name, value)
    {
    }

    /// <summary>Week 1 — sets of five; final set is 5+ (AMRAP).</summary>
    public static readonly Wendler531MesocycleWeek FiveRepWeek = new("5s Week", 1);

    /// <summary>Week 2 — sets of three; final set is 3+ (AMRAP).</summary>
    public static readonly Wendler531MesocycleWeek ThreeRepWeek = new("3s Week", 2);

    /// <summary>Week 3 — 5, 3, 1+ (AMRAP on the third work set).</summary>
    public static readonly Wendler531MesocycleWeek FiveThreeOneWeek = new("5/3/1 Week", 3);

    /// <summary>Week 4 — deload at reduced percentages, no AMRAP.</summary>
    public static readonly Wendler531MesocycleWeek DeloadWeek = new("Deload", 4);
}
