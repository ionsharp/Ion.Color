using Ion.Numeral;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Ion.Colors;

/// <summary>
/// <b>r, g, G</b>
/// <para>A color similar to <see cref="xyY"/> where a color is directly converted from <see cref="RGB"/> instead of <see cref="XYZ"/>.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="rgG"/></para>
/// </summary>
/// <remarks>https://en.wikipedia.org/wiki/Rg_chromaticity</remarks>
[ColorOf<RGB>]
[Component(1, "r")]
[Component(1, "g")]
[Component(1, '%', "G")]
[Description("A chromacity model similar to 'xyY' where a color is directly converted from 'RGB' instead of 'XYZ'.")]
[SuppressMessage("Style", "IDE1006:Naming Styles")]
public record class rgG(Double1 R, Double1 G1, Double1 G2)
    : Color3<rgG, Double1>(R, G1, G2), IColor3<rgG, Double1>, System.Numerics.IMinMaxValue<rgG>
{
    public static rgG MaxValue => new(1);

    public static rgG MinValue => new(0);

    public rgG() : this(default, default, default) { }

    public rgG(Double1 rgg) : this(rgg, rgg, rgg) { }

    public rgG(Double1 r, Double1 g) : this(r, g, MaxValue.Z) { }

    public rgG(in IVector2<Double1> rg) : this(rg.X, rg.Y, MaxValue.Z) { }

    public rgG(in IVector3<Double1> rgg) : this(rgg.X, rgg.Y, rgg.Z) { }

    public static explicit operator rgG(in RG input) => IColor.New<rgG>(input.X, input.Y, 1);

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="rgG"/></summary>
    public override void From(in Lrgb input, ColorProfile profile)
        => XYZ = input.XYZ.GetSum() is double sum && sum == 0 ? new Vector3<Double1>(0, 0, input.Y) : new(input.X / sum, input.Y / sum, input.Y);

    /// <summary><see cref="rgG"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
        => Y == 0 ? IColor.New<Lrgb>(0) : IColor.New<Lrgb>(X * Z / Y, Z, (1 - X - Y) * Z / Y);
}