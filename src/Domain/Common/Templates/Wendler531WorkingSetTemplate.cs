namespace ImposterSyndrome.Domain.Common.Templates;

/// <summary>
/// One main-lift work set in the classic template: percentage of training max, prescribed reps, and optional AMRAP.
/// </summary>
public sealed record Wendler531WorkingSetTemplate : IValueObject
{
    public Wendler531WorkingSetTemplate(int setOrder, decimal percentageOfTrainingMax, int prescribedReps, bool isAmrap)
    {
        ThrowIfLessThan(setOrder, 1, nameof(setOrder));
        ThrowIfLessThanOrEqual(percentageOfTrainingMax, 0m, nameof(percentageOfTrainingMax));
        ThrowIfGreaterThan(percentageOfTrainingMax, 1m, nameof(percentageOfTrainingMax));
        ThrowIfLessThan(prescribedReps, 1, nameof(prescribedReps));

        SetOrder = setOrder;
        PercentageOfTrainingMax = percentageOfTrainingMax;
        PrescribedReps = prescribedReps;
        IsAmrap = isAmrap;
    }

    /// <summary>1-based order within the session (typically three work sets).</summary>
    public int SetOrder { get; }

    /// <summary>Fraction of the lifter's training max (e.g. 0.65 for 65%).</summary>
    public decimal PercentageOfTrainingMax { get; }

    /// <summary>Target reps; when <see cref="IsAmrap"/> is true, this is the minimum before additional reps.</summary>
    public int PrescribedReps { get; }

    /// <summary>Whether the lifter may exceed <see cref="PrescribedReps"/> on this set (5+, 3+, 1+).</summary>
    public bool IsAmrap { get; }
}
