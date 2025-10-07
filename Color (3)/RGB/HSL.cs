using Ion.Numeral;
using System;

using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <b>Hue (H), Saturation (S), Lightness (L)</b>
/// <para>A model similar to 'HSB' where 'Lightness' (a perfectly light color is pure white) replaces 'Brightness' (a perfectly bright color is analogous to shining a white light on a colored object).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="HSL"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>HSD (Hue/Saturation/Darkness)</item>
/// <item>HSI (Hue/Saturation/Intensity)</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/hsl.js</remarks>
[ColorOf<Lrgb>]
[Component(360, '°', "H", "Hue")]
[Component(100, '%', "S", "Saturation")]
[Component(100, '%', "L", "Lightness")]
[ComponentGroup(ComponentGroup.H | ComponentGroup.HS | ComponentGroup.Lightness | ComponentGroup.SL)]
[Description("A model similar to 'HSB' where 'Lightness' (a perfectly light color is pure white) replaces 'Brightness' (a perfectly bright color is analogous to shining a white light on a colored object).")]
public record class HSL(double H, double S, double L)
    : Color3<HSL, double>(H, S, L), IColor3<HSL, double>, System.Numerics.IMinMaxValue<HSL>
{
    public static HSL MaxValue => new(360, 100, 100);

    public static HSL MinValue => new(0);

    public HSL() : this(default, default, default) { }

    public HSL(double hsl) : this(hsl, hsl, hsl) { }

    public HSL(IVector3<double> hsl) : this(hsl.X, hsl.Y, hsl.Z) { }

    /// <summary><see cref="HSL"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        var max = (Vector3)MaxValue;

        double h = XYZ.X / 60.0, s = XYZ.Y / max.Y, l = XYZ.Z / max.Z;

        double r = l, g = l, b = l;

        if (s > 0)
        {
            var chroma = (1.0 - (2.0 * l - 1.0).Abs()) * s;
            var x = chroma * (1.0 - ((h % 2.0) - 1).Abs());

            var result = new Vector(0.0, 0, 0);

            if (0 <= h && h <= 1)
            {
                result = new Vector(chroma, x, 0);
            }
            else if (1 <= h && h <= 2)
            {
                result = new Vector(x, chroma, 0);
            }
            else if (2 <= h && h <= 3)
            {
                result = new Vector(0.0, chroma, x);
            }
            else if (3 <= h && h <= 4)
            {
                result = new Vector(0.0, x, chroma);
            }
            else if (4 <= h && h <= 5)
            {
                result = new Vector(x, 0, chroma);
            }
            else if (5 <= h && h <= 6)
                result = new Vector(chroma, 0, x);

            var m = l - (0.5 * chroma);

            r = result[0] + m;
            g = result[1] + m;
            b = result[2] + m;
        }

        return IColor.New<Lrgb>(r, g, b);
    }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="HSL"/></summary>
    public override void From(in Lrgb input, ColorProfile profile)
    {
        var max = (Vector3)MaxValue;

        var m = Max(Max(input.XYZ.X, input.XYZ.Y), input.XYZ.Z);
        var n = Min(Min(input.XYZ.X, input.XYZ.Y), input.XYZ.Z);

        var chroma = m - n;

        double h = 0, s = 0, l = (m + n) / 2.0;

        if (chroma != 0)
        {
            s
                = l < 0.5
                ? chroma / (2.0 * l)
                : chroma / (2.0 - 2.0 * l);

            if (input.XYZ.X == m)
            {
                h = (input.XYZ.Y - input.XYZ.Z) / chroma;
                h = input.XYZ.Y < input.XYZ.Z
                ? h + 6.0
                : h;
            }
            else if (input.XYZ.Z == m)
            {
                h = 4.0 + ((input.XYZ.X - input.XYZ.Y) / chroma);
            }
            else if (input.XYZ.Y == m)
                h = 2.0 + ((input.XYZ.Z - input.XYZ.X) / chroma);

            h *= 60;
        }

        XYZ = new(h, s * max.Y, l * max.Z);
    }
}