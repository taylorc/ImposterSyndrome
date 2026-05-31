using ImposterSyndrome.Domain.Common.Enums;
using ImposterSyndrome.Domain.Common.Templates;

namespace ImposterSyndrome.Domain.Programme.Wendler531;

/// <summary>
/// Canonical Wendler 5/3/1 strength programme: four-week mesocycle, four main barbell lifts, training max from 90% of 1RM.
/// </summary>
[ValueObject<Guid>]
public readonly partial struct Wendler531ProgrammeId;

public sealed class Wendler531Programme : AggregateRoot<Wendler531ProgrammeId>
{
    public const string DefaultName = "5/3/1";
    public const int NameMaxLength = 120;

    /// <summary>Classic Wendler recommendation: training max = 90% of true or estimated one-rep max.</summary>
    public const decimal TrainingMaxFromOneRepMaxMultiplier = 0.90m;

    private string _name = null!;

    public string Name
    {
        get => _name;
        private set
        {
            ThrowIfNullOrWhiteSpace(value, nameof(Name));
            ThrowIfGreaterThan(value.Length, NameMaxLength, nameof(Name));
            _name = value;
        }
    }

    private Wendler531Programme()
    {
    }

    public static Wendler531Programme Create(string? name = null)
    {
        var resolved = string.IsNullOrWhiteSpace(name) ? DefaultName : name.Trim();
        return new Wendler531Programme
        {
            Id = Wendler531ProgrammeId.From(Guid.CreateVersion7()),
            Name = resolved,
        };
    }

    /// <summary>
    /// The four competition lifts used as main work in standard 5/3/1 (order is informational; scheduling is application concern).
    /// </summary>
    public static IReadOnlyList<BarbellExercise> MainLifts { get; } =
    [
        BarbellExercise.Squat,
        BarbellExercise.BenchPress,
        BarbellExercise.Deadlift,
        BarbellExercise.OverheadPress,
    ];

    /// <summary>Returns the three work sets for the given mesocycle week (classic percentages and rep scheme).</summary>
    public static IReadOnlyList<Wendler531WorkingSetTemplate> GetWorkingSetTemplates(Wendler531MesocycleWeek week)
    {
        ThrowIfNull(week);

        if (week == Wendler531MesocycleWeek.FiveRepWeek)
        {
            return
            [
                new Wendler531WorkingSetTemplate(1, 0.65m, 5, isAmrap: false),
                new Wendler531WorkingSetTemplate(2, 0.75m, 5, isAmrap: false),
                new Wendler531WorkingSetTemplate(3, 0.85m, 5, isAmrap: true),
            ];
        }

        if (week == Wendler531MesocycleWeek.ThreeRepWeek)
        {
            return
            [
                new Wendler531WorkingSetTemplate(1, 0.70m, 3, isAmrap: false),
                new Wendler531WorkingSetTemplate(2, 0.80m, 3, isAmrap: false),
                new Wendler531WorkingSetTemplate(3, 0.90m, 3, isAmrap: true),
            ];
        }

        if (week == Wendler531MesocycleWeek.FiveThreeOneWeek)
        {
            return
            [
                new Wendler531WorkingSetTemplate(1, 0.75m, 5, isAmrap: false),
                new Wendler531WorkingSetTemplate(2, 0.85m, 3, isAmrap: false),
                new Wendler531WorkingSetTemplate(3, 0.95m, 1, isAmrap: true),
            ];
        }

        if (week == Wendler531MesocycleWeek.DeloadWeek)
        {
            return
            [
                new Wendler531WorkingSetTemplate(1, 0.40m, 5, isAmrap: false),
                new Wendler531WorkingSetTemplate(2, 0.50m, 5, isAmrap: false),
                new Wendler531WorkingSetTemplate(3, 0.60m, 5, isAmrap: false),
            ];
        }

        throw new InvalidOperationException($"Unsupported mesocycle week: {week.Name}.");
    }

    /// <summary>Training max in the same unit as <paramref name="oneRepMax"/> (e.g. kilograms).</summary>
    public static decimal CalculateTrainingMax(decimal oneRepMax)
    {
        ThrowIfLessThanOrEqual(oneRepMax, 0m, nameof(oneRepMax));
        return decimal.Round(oneRepMax * TrainingMaxFromOneRepMaxMultiplier, 2, MidpointRounding.AwayFromZero);
    }

    /// <summary>Working weight for one set from training max and template percentage.</summary>
    public static decimal CalculateWorkingWeight(decimal trainingMax, decimal percentageOfTrainingMax)
    {
        ThrowIfLessThanOrEqual(trainingMax, 0m, nameof(trainingMax));
        ThrowIfLessThanOrEqual(percentageOfTrainingMax, 0m, nameof(percentageOfTrainingMax));
        ThrowIfGreaterThan(percentageOfTrainingMax, 1m, nameof(percentageOfTrainingMax));

        return decimal.Round(trainingMax * percentageOfTrainingMax, 2, MidpointRounding.AwayFromZero);
    }
}
