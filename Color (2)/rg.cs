using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>rg</b>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="rgG"/> ⇒ <see cref="RG"/></para>
/// </summary>
/// <remarks>https://en.wikipedia.org/wiki/Rg_chromaticity</remarks>
[Component(1, "r")]
[Component(1, "g")]
[Hide]
public record class RG(Double1 R, Double1 G)
    : Color2<RG, Double1, rgG>(R, G), IColor2<RG, Double1>, System.Numerics.IMinMaxValue<RG>
{
    public static RG MaxValue => new(Double1.MaxValue);

    public static RG MinValue => new(Double1.MinValue);

    public RG() : this(default, default) { }

    public RG(Double1 rg) : this(rg, rg) { }

    public RG(in IVector2<Double1> rg) : this(rg.X, rg.Y) { }

    public static explicit operator RG(Vector2 input) => IColor.New<RG>(input);

    public static explicit operator RG(rgG input) => new(input.XY);

    /// <see cref="IConvert{rgG}"/>

    /// <summary><see cref="rgG"/> ⇒ <see cref="RG"/></summary>
    public override void From(in rgG i, ColorProfile profile)
        => XY = i.XYZ.XY;

    /// <summary><see cref="RG"/> ⇒ <see cref="rgG"/></summary>
    public override void To(out rgG i, ColorProfile profile)
        => i = new(X, Y);
}