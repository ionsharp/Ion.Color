using Ion.Numeral;
using System;
using System.Globalization;

namespace Ion.Colors;

/// <summary>A set of colors that can be mixed in varying amounts to produce a gamut of color. </summary>
/// <remarks>https://en.wikipedia.org/wiki/Primary_color</remarks>
[Description(Description)]
public readonly record struct Primary3(in Vector2 R, in Vector2 G, in Vector2 B) : IFormattable
{
    public const string Description = "A set of colors that can be mixed in varying amounts to produce a gamut of color.";

    public const string StringFormat = "R = ({0}), G = ({1}), B = ({2})";

    public readonly Vector2 R { get; } = new(R);

    public readonly Vector2 G { get; } = new(G);

    public readonly Vector2 B { get; } = new(B);

    public Primary3(double rX, double rY, double gX, double gY, double bX, double bY) : this(new Vector2(rX, rY), new Vector2(gX, gY), new Vector2(bX, bY)) { }

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => StringFormat.F(R.ToString(format, provider), G.ToString(format, provider), B.ToString(format, provider));
}