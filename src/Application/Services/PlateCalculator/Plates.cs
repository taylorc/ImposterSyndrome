using System;
using System.Collections.Generic;
using System.Text;

namespace ImposterSyndrome.Application.Services.PlateCalculator;

public class Plates
{
    public int Number { get; }
    public int PerSide => Number / 2;

    public Plates(int number, Plate plate)
    {
        Number = number;
        Plate = plate;
    }

    public Plate Plate { get; }


}
