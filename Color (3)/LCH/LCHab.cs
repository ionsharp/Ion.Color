using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Luminance (L), Chroma (C), Hue (H)</b>
/// <para>A cylindrical form of 'Lab' that is designed to accord with the human perception of color.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Lab"/> ⇒ <see cref="LCHab"/></para>
/// 
/// </summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
[ComponentGroup(ComponentGroup.LCH)]
[Description("A cylindrical form of 'Lab' that is designed to accord with the human perception of color.")]
public record class LCHab(double X, double Y, double Z)
    : LCH<LCHab, Lab>(X, Y, Z), IColor3<LCHab, double>, System.Numerics.IMinMaxValue<LCHab>
{
    public static LCHab MaxValue => new(100, 100, 360);

    public static LCHab MinValue => new(0);

    public LCHab() : this(default, default, default) { }

    public LCHab(double lch) : this(lch, lch, lch) { }

    public LCHab(IVector3<double> lch) : this(lch.X, lch.Y, lch.Z) { }
}