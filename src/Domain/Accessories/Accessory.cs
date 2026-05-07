using Ardalis.GuardClauses;
using ImposterSyndrome.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ImposterSyndrome.Domain.Accessories;

[ValueObject<Guid>]
public readonly partial struct AccessoryId;

public class Accessory: AggregateRoot<AccessoryId>
{
    public const int NameMaxLength = 100;

    public BarbellExercise BarbellExercise { get; set; }

    public string Name
    {
        get;
        set
        {
            Guard.Against.NullOrWhiteSpace(value);
            Guard.Against.StringTooLong(value, NameMaxLength, message: $"Name cannot be greater than {NameMaxLength} characters");
            field = value;
        }
    } = null!;
    private Accessory() { } // Needed for EF Core

    public static Accessory Create(string name, BarbellExercise barbellExercise)
    {
        var accessory = new Accessory { 
            Id = AccessoryId.From(Guid.CreateVersion7()), 
            Name = name, BarbellExercise = barbellExercise 
        };

        return accessory;
    }
}
