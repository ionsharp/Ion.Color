using System;

namespace Ion.Colors;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ComponentGroupAttribute(ComponentGroup group) : Attribute()
{
    public const string DescriptionPrefix = "A color with ";

    public ComponentGroup Group { get; set; } = group;
}