using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImposterSyndrome.Domain.Common.Enums;

public class BarbellExercise : SmartEnum<BarbellExercise, string>
{
    public BarbellExercise(string name, string value) : base(name, value)
    {
    }

    public static readonly BarbellExercise Squat = new("Squat", "Squat");
    public static readonly BarbellExercise Deadlift = new("Deadlift", "Deadlift");
    public static readonly BarbellExercise BenchPress = new("BenchPress", "BenchPress");
    public static readonly BarbellExercise OverheadPress = new("OverheadPress", "OverheadPress");
}
