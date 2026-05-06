using ImposterSyndrome.Application.Services.PlateCalculator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImposterSyndrome.Application.Services.Interfaces;

public interface IPlateCalculator
{
    List<Plates> GetPlates(double weightNeeded, double barWeight = 20);
}
