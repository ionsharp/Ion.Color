using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Hue (H), Saturation (P), Lightness (L)</b>
/// <para>A model derived from 'LCHuv' that defines color as having hue (H), saturation (P), and lightness (L).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Luv"/> ⇒ <see cref="LCHuv"/> ⇒ <see cref="HPLuv"/></para>
/// </summary>
/// <remarks>https://github.com/hsluv/hsluv-csharp</remarks>
[ColorOf<LCHuv>]
[Component(360, '°', "H", "Hue")]
[Component(100, '%', "P", "Saturation")]
[Component(100, '%', "L", "Lightness")]
[ComponentGroup(ComponentGroup.H | ComponentGroup.HS | ComponentGroup.Lightness | ComponentGroup.SL)]
[Description("A model derived from 'LCHuv' that defines color as having hue (H), saturation (P), and lightness (L).")]
public record class HPLuv(double H, double P, double L)
    : HLuv<HPLuv>(H, P, L), IColor3<HPLuv, double>, System.Numerics.IMinMaxValue<HPLuv>
{
    public static HPLuv MaxValue => new(360, 100, 100);

    public static HPLuv MinValue => new(0);

    public HPLuv() : this(default, default, default) { }

    public HPLuv(double hpl) : this(hpl, hpl, hpl) { }

    public HPLuv(IVector3<double> hpl) : this(hpl.X, hpl.Y, hpl.Z) { }

    /// <summary><see cref="LCHuv"/> ⇒ <see cref="HPLuv"/></summary>
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

        double max = GetChroma(L);
        double S = C / max * 100;

        XYZ = new(H, S, L);
    }

    /// <summary><see cref="HPLuv"/> ⇒ <see cref="LCHuv"/></summary>
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

        double max = GetChroma(L);
        double C = max / 100 * S;

        result = IColor.New<LCHuv>(L, C, H);
    }
}