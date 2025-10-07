using Ion.Numeral;
using System;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <b>Hue (H), Chroma (C), Luminance (Y)</b>
/// <para>A model that defines color as having hue (H), chroma (C), and luminance (Y).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="HCY"/></para>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/hcy.js</remarks>
[ColorOf<Lrgb>]
[Component(360, '°', "H", "Hue")]
[Component(100, '%', "C", "Chroma")]
[Component(255, ' ', "Y", "Luminance")]
[ComponentGroup(ComponentGroup.H | ComponentGroup.HC | ComponentGroup.Luminance)]
[Description("A model that defines color as having hue (H), chroma (C), and luminance (Y).")]
public record class HCY(double H, double C, double Y)
    : Color3<HCY, double>(H, C, Y), IColor3<HCY, double>, System.Numerics.IMinMaxValue<HCY>
{
    public static HCY MaxValue => new(360, 100, 255);

    public static HCY MinValue => new(0);

    public HCY() : this(default, default, default) { }

    public HCY(double hcy) : this(hcy, hcy, hcy) { }

    public HCY(IVector3<double> hcy) : this(hcy.X, hcy.Y, hcy.Z) { }

    /// <summary><see cref="HCY"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        var h = (X < 0 ? (X % 360) + 360 : (X % 360)) * PI / 180;
        var c = Y / 100;
        var y = Z / 255;

        var pi3 = PI / 3; //Do not confuse with PI * 3!

        double r, g, b;
        if (h < (2 * pi3))
        {
            b = y * (1 - c);
            r = y * (1 + (c * Cos(h) / Cos(pi3 - h)));
            g = y * (1 + (c * (1 - Cos(h) / Cos(pi3 - h))));
        }
        else if (h < (4 * pi3))
        {
            h -= 2 * pi3;
            r = y * (1 - c);
            g = y * (1 + (c * Cos(h) / Cos(pi3 - h)));
            b = y * (1 + (c * (1 - Cos(h) / Cos(pi3 - h))));
        }
        else
        {
            h -= 4 * pi3;
            g = y * (1 - c);
            b = y * (1 + (c * Cos(h) / Cos(pi3 - h)));
            r = y * (1 + (c * (1 - Cos(h) / Cos(pi3 - h))));
        }
        return IColor.New<Lrgb>(r, g, b);
    }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="HCY"/></summary>
    public override void From(in Lrgb input, ColorProfile profile)
    {
        var sum = input.X + input.Y + input.Z;

        var r = input.X / sum;
        var g = input.Y / sum;
        var b = input.Z / sum;

        var h = Acos((0.5 * ((r - g) + (r - b))) / Sqrt((r - g) * (r - g) + (r - b) * (g - b)));

        if (b > g)
            h = 2 * PI - h;

        var c = 1 - 3 * Min(r, Min(g, b));

        var y = sum / 3;
        XYZ = new(h * 180 / PI, c * 100, y * 255);
    }
}