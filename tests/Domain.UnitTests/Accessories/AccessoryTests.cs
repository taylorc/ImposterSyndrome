using ImposterSyndrome.Domain.Accessories;
using ImposterSyndrome.Domain.Common.Enums;
using ImposterSyndrome.Domain.Heroes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImposterSyndrome.Domain.UnitTests.Accessories;


public class AccessoryTests
{
    [Theory]
    [InlineData("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "cc3431a8-4a31-4f76-af64-e8198279d7a4", false)]
    [InlineData("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "c8ad9974-ca93-44a5-9215-2f4d9e866c7a", true)]
    public void AccessoryId_ShouldBeComparable(string stringGuid1, string stringGuid2, bool isEqual)
    {
        // Arrange
        Guid guid1 = Guid.Parse(stringGuid1);
        Guid guid2 = Guid.Parse(stringGuid2);
        AccessoryId id1 = AccessoryId.From(guid1);
        AccessoryId id2 = AccessoryId.From(guid2);
        // Act
        var areEqual = id1 == id2;
        // Assert
        areEqual.Should().Be(isEqual);
        id1.Value.Should().Be(guid1);
        id2.Value.Should().Be(guid2);
    }

    [Fact]
    public void Create_WithValidName_ShouldSucceed()
    {
        // Arrange
        var name = "name";
        var barBellExercise = BarbellExercise.BenchPress;

        // Act
        var accessory = Accessory.Create(name, barBellExercise);

        // Assert
        accessory.Should().NotBeNull();
        accessory.Name.Should().Be(name);
        accessory.BarbellExercise.Should().Be(barBellExercise);
    }

    [Fact]
    public void Create_WithNameGreaterThan100Characters_ShouldFail()
    {
        // Arrange
        var name = new string('a', 101);
        var barBellExercise = BarbellExercise.BenchPress;

        // Act
        Action act = () => Accessory.Create(name, barBellExercise);
        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Name cannot be greater than 100 characters (Parameter 'value')");
    }

    [Fact]
    public void Create_WithNameNull_ShouldFail()
    {
        // Arrange
        var name = string.Empty;
        var barBellExercise = BarbellExercise.BenchPress;

        // Act
        Action act = () => Accessory.Create(name, barBellExercise);
        // Assert
        act.Should().Throw<ArgumentException>();
    }
}
