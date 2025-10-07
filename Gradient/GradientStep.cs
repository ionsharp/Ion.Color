using Ion.Core;
using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>The color and position of a transition point in a gradient.</summary>
[Description(Description)]
public record class GradientStep() : Model()
{
    public const string Description = "The color and position of a transition point in a gradient.";

    public const string StringFormat = "{0} ({1})";

    public ByteVector4 Color { get => this.Get<ByteVector4>(); set => this.Set(value); }

    public Double1 Offset { get => this.Get<Double1>(); set => this.Set(value); }

    public GradientStep(Double1 offset, ByteVector4 color) : this() { Offset = offset; Color = color; }

    public override string ToString(string format, IFormatProvider provider) => StringFormat.F(Offset, Color.ToString(format, provider));
}