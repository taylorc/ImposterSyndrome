
using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImposterSyndrome.Application.Services.PlateCalculator;

public class Plate : SmartEnum<Plate, double>
{
    public static readonly Plate TwentyFive = new("25", 25);
    public static readonly Plate Twenty = new("20", 20);
    public static readonly Plate Fifteen = new("15", 15);
    public static readonly Plate Ten = new("10", 10);
    public static readonly Plate Five = new("5", 5);
    public static readonly Plate TwoPointFive = new("2.5", 2.5);
    public static readonly Plate OnePointTwoFive = new("1.25", 1.25);
    public static readonly Plate ZeroPointFive = new("0.5", 0.5);
    public static readonly Plate ZeroPointTwoFive = new("0.25", 0.25);

    /*
     TWO x 0.25kg Calibrated Steel Weight Plates
TWO x 0.5kg Calibrated Steel Weight Plates
TWO x 1.25kg Calibrated Steel Weight Plates
TWO x 2.5kg Calibrated Steel Weight Plates
    */

    public Plate(string name, double value) : base(name, value)
    {
    }

}
