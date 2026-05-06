using AwesomeAssertions;
using ImposterSyndrome.Application.Services.Interfaces;
using ImposterSyndrome.Application.Services.PlateCalculator;

namespace ImposterSyndrome.Application.UnitTests.Services.PlateCalculator;

public class PlateCalculatorTests
{
    [Fact]
    public void CalculatesCorrectPlates()
    {
        double weight = 70; // Example weight
        List<Plates> plates =
        [
            // Example expected plates calculation
            new Plates(2, Plate.TwentyFive),
        ];

        IPlateCalculator calculator = new Application.Services.PlateCalculator.PlateCalculator();
        calculator.GetPlates(weight).Should().BeEquivalentTo(plates);
    }

    [Fact]
    public void CalculatesCorrectPlatesTwo()
    {
        double weight = 265; // Example weight

        //305
        List<Plates> plates =
        [
            // Example expected plates calculation
            new Plates(6, Plate.TwentyFive), //150 left 95
            new Plates(2, Plate.Twenty),    // 40 left 55
            new Plates(2, Plate.Fifteen),   // 30 left 25
            new Plates(2, Plate.Ten),       // 20 left 5
            new Plates(2, Plate.TwoPointFive), // 5 left 40
        ];

        IPlateCalculator calculator = new Application.Services.PlateCalculator.PlateCalculator();
        calculator.GetPlates(weight).Should().BeEquivalentTo(plates);
    }
}
