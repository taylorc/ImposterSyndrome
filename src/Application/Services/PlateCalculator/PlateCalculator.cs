using ImposterSyndrome.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImposterSyndrome.Application.Services.PlateCalculator;

public class PlateCalculator : IPlateCalculator
{
    private List<Plates> availablePlates = new()
    {
        new Plates(6, Plate.TwentyFive),
        new Plates(2,Plate.Twenty),
        new Plates(2,Plate.Fifteen),
        new Plates(2,Plate.Ten),
        new Plates(2,Plate.Five),
        new Plates(2,Plate.TwoPointFive),
        new Plates(2,Plate.OnePointTwoFive),
        new Plates(2,Plate.ZeroPointFive),
        new Plates(2,Plate.ZeroPointTwoFive)
    };

    private double MaxWeight()
    {
        double total = 20; // Bar weight
        foreach (var plate in availablePlates)
        {
            total += plate.Number * plate.Plate.Value;
        }
        return total;
    }

    public List<Plates> GetPlates(double weightNeeded, double barWeight = 20)
    {
        if(weightNeeded > MaxWeight())
        {
            throw new ArgumentException("Weight needed exceeds maximum available weight with current plates.");
        }

        double platesWeight = weightNeeded - barWeight;

        List<Plates> platesToUse = new();

        decimal weightPerSide = (decimal)(platesWeight / 2);

        foreach (var plate in availablePlates)
        {
            int platesCount = 0;
            decimal plateValue = (decimal)plate.Plate.Value;

            while (weightPerSide >= plateValue && plate.PerSide - platesCount > 0)
            {
                weightPerSide -= plateValue;
                platesCount++;
            }
            if (platesCount > 0)
            {
                platesToUse.Add(new Plates(platesCount*2, plate.Plate));
            }
        }

        return platesToUse;


    }
}
