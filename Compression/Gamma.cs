using Ion.Numeral;
using System;
using System.Globalization;
using static System.Math;

namespace Ion.Colors;

/// <summary><b>Gamma</b></summary>
[Name(Name)]
public readonly record struct GammaCompression() : ICompress
{
    public const string Name = "Gamma";

    public const string StringFormat = "{0} ({1})";

    public readonly double Gamma { get; } = 2.4;

    public GammaCompression(double gamma) : this() => Gamma = gamma;

    public readonly double Transfer(double channel)
    {
        var v = channel;
        var V = Pow(v, 1 / Gamma);
        return V;
    }

    public readonly double TransferInverse(double channel)
    {
        var V = channel;
        var v = Pow(V, Gamma);
        return v;
    }

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => StringFormat.F(Name, Gamma);
}