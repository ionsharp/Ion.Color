using Ion.Numeral;
using System;
using System.Globalization;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <b>Hybrid Log-Gamma (HLG)</b>
/// <para>A hybrid transfer function developed by NHK and BBC for HDR and offering some backward compatibility on SDR displays. The lower half of the signal values use a gamma curve and the upper half of the signal values use a logarithmic curve.</para>
/// <para><b>Rec. 2100</b></para>
/// </summary>
/// <remarks>https://en.wikipedia.org/wiki/Hybrid_log%E2%80%93gamma</remarks>
[Name(Name)]
public readonly record struct GammaLogCompression() : ICompress
{
    public const string Name = "Gamma (HLG)";

    public readonly double Transfer(double E)
    {
        var r = 0.5;

        var a = 0.17883277;
        var b = 1 - 4 * a;
        var c = 0.5 - a * Log(4 * a);

        if (E >= 0 && E <= 1)
            return r * Sqrt(E);

        return a * Log(E - b) + c; //1 < E
    }

    public readonly double TransferInverse(double E)
    {
        var a = 0.17883277;
        var b = 1 - 4 * a;
        var c = 0.5 - a * Log(4 * a);

        if (E >= 0 && E <= 1 / 12)
            return Sqrt(3 * E);

        return a * Log(12 * E - b) + c; //1 / 12 < E <= 1
    }

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => Name;
}