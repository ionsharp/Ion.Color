using Ion.Numeral;
using System;

using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <para><b>Hue (H), Saturation (S), Brightness (B)</b></para>
/// <para>A model that defines color as having hue (H), saturation (S), and brightness (B).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="HSB"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>HSV</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/hsb.js</remarks>
[ColorOf<Lrgb>]
[Component(360, '°', "H", "Hue")]
[Component(100, '%', "S", "Saturation")]
[Component(100, '%', "B", "Brightness")]
[ComponentGroup(ComponentGroup.H | ComponentGroup.HS | ComponentGroup.SB)]
[Description("A model that defines color as having hue (H), saturation (S), and brightness (B).")]
public record class HSB(double H, double S, double B)
    : Color3<HSB, double>(H, S, B), IColor3<HSB, double>, System.Numerics.IMinMaxValue<HSB>
{
    public static HSB MaxValue => new(360, 100, 100);

    public static HSB MinValue => new(0);

    public HSB() : this(default, default, default) { }

    public HSB(double hsb) : this(hsb, hsb, hsb) { }

    public HSB(IVector3<double> hsb) : this(hsb.X, hsb.Y, hsb.Z) { }

    /// <summary><see cref="HSB"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        var hsb = new Vector(XYZ.X / 360, XYZ.Y / 100, XYZ.Z / 100);
        if (hsb[1] == 0)
            return IColor.New<Lrgb>(hsb[2], hsb[2], hsb[2]);

        var h = hsb[0] * 6;
        if (h == 6)
            h = 0;

        var i = Floor(h);
        var x = hsb[2] * (1 - hsb[1]);
        var y = hsb[2] * (1 - hsb[1] * (h - i));
        var z = hsb[2] * (1 - hsb[1] * (1 - (h - i)));

        double r, g, b;

        if (i == 0)
        { r = hsb[2]; g = z; b = x; }
        else if (i == 1)
        { r = y; g = hsb[2]; b = x; }
        else if (i == 2)
        { r = x; g = hsb[2]; b = z; }
        else if (i == 3)
        { r = x; g = y; b = hsb[2]; }
        else if (i == 4)
        { r = z; g = x; b = hsb[2]; }
        else
        { r = hsb[2]; g = x; b = y; }

        return IColor.New<Lrgb>(r, g, b);
    }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="HSB"/></summary>
    public override void From(in Lrgb input, ColorProfile profile)
    {
        var max = (Vector3)MaxValue;

        var r = input.X; var g = input.Y; var b = input.Z;

        var m = Max(r, Max(g, b));
        var n = Min(r, Min(g, b));

        var chroma = m - n;

        double h = 0, s, v = m;
        if (chroma == 0)
        {
            h = 0; s = 0;
        }
        else
        {
            s = chroma / m;

            var dR = (((m - r) / 6) + (chroma / 2)) / chroma;
            var dG = (((m - g) / 6) + (chroma / 2)) / chroma;
            var dB = (((m - b) / 6) + (chroma / 2)) / chroma;

            if (r == m)
                h = dB - dG;

            else if (g == m)
                h = (1 / 3) + dR - dB;

            else if (b == m)
                h = (2 / 3) + dG - dR;

            if (h < 0)
                h += 1;

            if (h > 1)
                h -= 1;
        }

        XYZ = new Vector3(h, s, v) * max;
    }
}