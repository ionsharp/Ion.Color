using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Hue (H), Saturation (S), Lightness (L)</b>
/// <para>A model derived from 'LCHuv' that defines color as having hue (H), saturation (S), and lightness (L).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Luv"/> ⇒ <see cref="LCHuv"/> ⇒ <see cref="HSLuv"/></para>
/// </summary>
/// <remarks>https://github.com/hsluv/hsluv-csharp</remarks>
[ColorOf<LCHuv>]
[Component(360, '°', "H", "Hue")]
[Component(100, '%', "S", "Saturation")]
[Component(100, '%', "L", "Lightness")]
[ComponentGroup(ComponentGroup.H | ComponentGroup.HS | ComponentGroup.Lightness | ComponentGroup.SL)]
[Description("A model derived from 'LCHuv' that defines color as having hue (H), saturation (S), and lightness (L).")]
public record class HSLuv(double H, double S, double L)
    : HLuv<HSLuv>(H, S, L), IColor3<HSLuv, double>, System.Numerics.IMinMaxValue<HSLuv>
{
    public static HSLuv MaxValue => new(360, 100, 100);

    public static HSLuv MinValue => new(0);

    public HSLuv() : this(default, default, default) { }

    public HSLuv(double hsl) : this(hsl, hsl, hsl) { }

    public HSLuv(IVector3<double> hsl) : this(hsl.X, hsl.Y, hsl.Z) { }

    /// <summary><see cref="LCHuv"/> ⇒ <see cref="HSLuv"/></summary>
    public override void From(in LCHuv input, ColorProfile profile)
    {
        double L = input.X, C = input.Y, H = input.Z;

        if (L > 99.9999999)
        {
            XYZ = new(H, 0, 100);
            return;
        }

        if (L < 0.00000001)
        {
            XYZ = new(H, 0, 0);
            return;
        }

        double max = GetChroma(L, H);
        double S = C / max * 100;

        XYZ = new(H, S, L);
    }

    /// <summary><see cref="HSLuv"/> ⇒ <see cref="LCHuv"/></summary>
    public override void To(out LCHuv result, ColorProfile profile)
    {
        double H = XYZ.X, S = XYZ.Y, L = XYZ.Z;

        if (L > 99.9999999)
        {
            result = IColor.New<LCHuv>(100, 0, H);
            return;
        }

        if (L < 0.00000001)
        {
            result = IColor.New<LCHuv>(0, 0, H);
            return;
        }

        double max = GetChroma(L, H);
        double C = max / 100 * S;

        result = IColor.New<LCHuv>(L, C, H);
    }
}