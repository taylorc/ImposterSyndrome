using ErrorOr;
using ImposterSyndrome.Domain.Teams;

namespace ImposterSyndrome.Domain.UnitTests.Teams;

public class MissionTests
{
    [Theory]
    [InlineData("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "cc3431a8-4a31-4f76-af64-e8198279d7a4", false)]
    [InlineData("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "c8ad9974-ca93-44a5-9215-2f4d9e866c7a", true)]
    public void MissionId_ShouldBeComparable(string stringGuid1, string stringGuid2, bool isEqual)
    {
        // Arrange
        Guid guid1 = Guid.Parse(stringGuid1);
        Guid guid2 = Guid.Parse(stringGuid2);
        MissionId id1 = MissionId.From(guid1);
        MissionId id2 = MissionId.From(guid2);

        // Act
        var areEqual = id1 == id2;

        // Assert
        areEqual.Should().Be(isEqual);
        id1.Value.Should().Be(guid1);
        id2.Value.Should().Be(guid2);
    }

    [Fact]
    public void Complete_WhenStatusIsNotComplete_UpdatesStatusAndReturnsSuccess()
    {
        // Arrange
        var mission = Mission.Create("Test mission");

        // Act
        var result = mission.Complete();

        // Assert
        Assert.Equal(MissionStatus.Complete, mission.Status);
        Assert.IsType<Success>(result.Value);
    }

    [Fact]
    public void Complete_WhenStatusIsAlreadyComplete_ReturnsAlreadyCompletedError()
    {
        // Arrange
        var mission = Mission.Create("Test mission");
        mission.Complete(); // Set status to Complete

        // Act
        var result = mission.Complete();

        // Assert
        Assert.Equal(MissionStatus.Complete, mission.Status);
        Assert.True(result.IsError);
        Assert.Equal(MissionErrors.AlreadyCompleted, result.Errors[0]);
    }
}