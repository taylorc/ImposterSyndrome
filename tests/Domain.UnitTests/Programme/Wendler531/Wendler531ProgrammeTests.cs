using ImposterSyndrome.Domain.Common.Enums;
using ImposterSyndrome.Domain.Programme.Wendler531;

namespace ImposterSyndrome.Domain.UnitTests.Programme.Wendler531;

public sealed class Wendler531ProgrammeTests
{
    [Fact]
    public void Create_WithNullName_ShouldUseDefaultName()
    {
        var programme = Wendler531Programme.Create(null);

        programme.Name.Should().Be(Wendler531Programme.DefaultName);
    }

    [Fact]
    public void Create_WithWhitespaceName_ShouldUseDefaultName()
    {
        var programme = Wendler531Programme.Create("   ");

        programme.Name.Should().Be(Wendler531Programme.DefaultName);
    }

    [Fact]
    public void Create_WithCustomName_ShouldTrim()
    {
        var programme = Wendler531Programme.Create("  My Cycle  ");

        programme.Name.Should().Be("My Cycle");
    }

    [Fact]
    public void MainLifts_ShouldContainFourCanonicalLifts()
    {
        Wendler531Programme.MainLifts.Should().HaveCount(4);
        Wendler531Programme.MainLifts.Should().Contain(BarbellExercise.Squat);
        Wendler531Programme.MainLifts.Should().Contain(BarbellExercise.BenchPress);
        Wendler531Programme.MainLifts.Should().Contain(BarbellExercise.Deadlift);
        Wendler531Programme.MainLifts.Should().Contain(BarbellExercise.OverheadPress);
    }

    [Fact]
    public void GetWorkingSetTemplates_FiveRepWeek_ShouldMatchClassicPercentagesAndAmrap()
    {
        var sets = Wendler531Programme.GetWorkingSetTemplates(Wendler531MesocycleWeek.FiveRepWeek);

        sets.Should().HaveCount(3);
        sets[0].SetOrder.Should().Be(1);
        sets[0].PercentageOfTrainingMax.Should().Be(0.65m);
        sets[0].PrescribedReps.Should().Be(5);
        sets[0].IsAmrap.Should().BeFalse();
        sets[1].SetOrder.Should().Be(2);
        sets[1].PercentageOfTrainingMax.Should().Be(0.75m);
        sets[1].IsAmrap.Should().BeFalse();
        sets[2].SetOrder.Should().Be(3);
        sets[2].PercentageOfTrainingMax.Should().Be(0.85m);
        sets[2].IsAmrap.Should().BeTrue();
    }

    [Fact]
    public void GetWorkingSetTemplates_ThreeRepWeek_ShouldMatchClassicTemplate()
    {
        var sets = Wendler531Programme.GetWorkingSetTemplates(Wendler531MesocycleWeek.ThreeRepWeek);

        sets.Should().HaveCount(3);
        sets.Select(s => s.PercentageOfTrainingMax).Should().Equal(0.70m, 0.80m, 0.90m);
        sets.Should().OnlyContain(s => s.PrescribedReps == 3);
        sets.Take(2).Should().OnlyContain(s => !s.IsAmrap);
        sets.Last().IsAmrap.Should().BeTrue();
    }

    [Fact]
    public void GetWorkingSetTemplates_FiveThreeOneWeek_ShouldBe531Reps()
    {
        var sets = Wendler531Programme.GetWorkingSetTemplates(Wendler531MesocycleWeek.FiveThreeOneWeek);

        sets.Should().HaveCount(3);
        sets[0].PrescribedReps.Should().Be(5);
        sets[1].PrescribedReps.Should().Be(3);
        sets[2].PrescribedReps.Should().Be(1);
        sets[2].IsAmrap.Should().BeTrue();
        sets.Select(s => s.PercentageOfTrainingMax).Should().Equal(0.75m, 0.85m, 0.95m);
    }

    [Fact]
    public void GetWorkingSetTemplates_DeloadWeek_ShouldHaveNoAmrap()
    {
        var sets = Wendler531Programme.GetWorkingSetTemplates(Wendler531MesocycleWeek.DeloadWeek);

        sets.Should().HaveCount(3);
        sets.Should().OnlyContain(s => !s.IsAmrap && s.PrescribedReps == 5);
        sets.Select(s => s.PercentageOfTrainingMax).Should().Equal(0.40m, 0.50m, 0.60m);
    }

    [Fact]
    public void GetWorkingSetTemplates_WithNullWeek_ShouldThrow()
    {
        var act = () => Wendler531Programme.GetWorkingSetTemplates(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CalculateTrainingMax_ShouldBeNinetyPercentRounded()
    {
        Wendler531Programme.CalculateTrainingMax(100m).Should().Be(90.00m);
        Wendler531Programme.CalculateTrainingMax(200m).Should().Be(180.00m);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CalculateTrainingMax_WhenNotPositive_ShouldThrow(decimal oneRepMax)
    {
        var act = () => Wendler531Programme.CalculateTrainingMax(oneRepMax);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void CalculateWorkingWeight_ShouldMultiplyAndRound()
    {
        Wendler531Programme.CalculateWorkingWeight(100m, 0.65m).Should().Be(65.00m);
        Wendler531Programme.CalculateWorkingWeight(90m, 0.85m).Should().Be(76.50m);
    }

    [Theory]
    [InlineData(0, 0.5)]
    [InlineData(100, 0)]
    [InlineData(100, 1.01)]
    public void CalculateWorkingWeight_WhenInvalid_ShouldThrow(decimal tm, decimal pct)
    {
        var act = () => Wendler531Programme.CalculateWorkingWeight(tm, pct);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}
