using Ion.Numeral;
using System;
using System.Globalization;

namespace Ion.Colors;

#pragma warning disable IDE1006
/// <summary><b>Default</b></summary>
[Name(Name)]
public readonly record struct Compression() : ICompress
{
    public const string Name = "Default";

    /// <summary>γ</summary>
    public readonly double γ { get; } = 12 / 5;

    /// <summary>α</summary>
    public readonly double α { get; } = 0.055;

    /// <summary>β</summary>
    public readonly double β { get; } = 0.0031308;

    /// <summary>δ</summary>
    public readonly double δ { get; } = 12.92;

    /// <summary>βδ</summary>
    public readonly double βδ { get; } = 0.04045;

    public Compression(double γ, double α, double β, double δ, double βδ) : this()
    {
        this.γ = γ; this.α = α; this.β = β; this.δ = δ; this.βδ = βδ;
    }

    #region L* (To do: cross-reference and identify discrepancies)

    /*
    public double TransferInverse(double channel)
    {
        var V = channel;
        var v = V <= 0.08 ? 100 * V / CIE.IKappa : Pow3((V + 0.16) / 1.16);
        return v;
    }

    public double Transfer(double channel)
    {
        var v = channel;
        var V = v <= CIE.IEpsilon ? v * CIE.IKappa / 100d : 1.16 * Math.Pow(v, 1.0 / 3.0) - 0.16;
        return V;
    }
    */

    #endregion

    public readonly double Transfer(double channel)
    {
        var v = channel;
        var V = v <= β ? δ * v : (α + 1) * Math.Pow(v, 1 / γ) - α;
        return V;
    }

    public readonly double TransferInverse(double channel)
    {
        var V = channel;
        var v = V <= βδ ? V / δ : Math.Pow((V + α) / (α + 1), γ);
        return v;
    }

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => Name;
}
#pragma warning restore