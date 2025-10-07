using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Luminance (L), Chroma (C), Hue (H)</b>
/// <para>A cylindrical form of 'Labh' that is designed to accord with the human perception of color.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Labh"/> ⇒ <see cref="LCHabh"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>Hunter LCHab</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
[ComponentGroup(ComponentGroup.LCH)]
[Description("A cylindrical form of 'Labh' that is designed to accord with the human perception of color.")]
public record class LCHabh(double X, double Y, double Z)
    : LCH<LCHabh, Labh>(X, Y, Z), IColor3<LCHabh, double>, System.Numerics.IMinMaxValue<LCHabh>
{
    public static LCHabh MaxValue => new(100, 100, 360);

    public static LCHabh MinValue => new(0);

    public LCHabh() : this(default, default, default) { }

    public LCHabh(double lch) : this(lch, lch, lch) { }

    public LCHabh(IVector3<double> lch) : this(lch.X, lch.Y, lch.Z) { }
}