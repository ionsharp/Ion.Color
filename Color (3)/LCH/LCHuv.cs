using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Luminance (L), Chroma (C), Hue (H)</b>
/// <para>A cylindrical form of 'Luv' that is designed to accord with the human perception of color.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Luv"/> ⇒ <see cref="LCHuv"/></para>
/// </summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
[ComponentGroup(ComponentGroup.LCH)]
[Description("A cylindrical form of 'Luv' that is designed to accord with the human perception of color.")]
public record class LCHuv(double X, double Y, double Z)
    : LCH<LCHuv, Luv>(X, Y, Z), IColor3<LCHuv, double>, System.Numerics.IMinMaxValue<LCHuv>
{
    public static LCHuv MaxValue => new(100, 100, 360);

    public static LCHuv MinValue => new(0);

    public LCHuv() : this(default, default, default) { }

    public LCHuv(double lch) : this(lch, lch, lch) { }

    public LCHuv(IVector3<double> lch) : this(lch.X, lch.Y, lch.Z) { }
}