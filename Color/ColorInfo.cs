using Ion.Reflect;
using System;
using System.Collections.Generic;

namespace Ion.Colors;

public record class ColorInfo(Type Type)
{
    public IReadOnlyCollection<Component> Components { get; } = [.. IColor.Components[Type]];

    public string Description { get; } = Type.GetAttribute<DescriptionAttribute>()?.Description ?? "";

    public string Name => Type.Name;

    public Type Type { get; } = Type;
}