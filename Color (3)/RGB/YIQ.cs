using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Y, I, Q</b>
/// <para>The color model used by the analog NTSC color TV system.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="YIQ"/></para>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/yiq.js</remarks>
[ColorOf<Lrgb>]
[Component(1, '%', "Y", "Luminance")]
[Component(-0.5957, 0.5957, ' ', "I")]
[Component(-0.5226, 0.5226, ' ', "Q")]
[ComponentGroup(ComponentGroup.Luminance)]
[Description("The color model used by the analog NTSC color TV system.")]
public record class YIQ(double Y, double I, double Q)
    : Color3<YIQ, double>(Y, I, Q), IColor3<YIQ, double>, System.Numerics.IMinMaxValue<YIQ>
{
    public static YIQ MaxValue => new(1, +0.5957, +0.5226);

    public static YIQ MinValue => new(0, -0.5957, -0.5226);

    public static readonly Matrix3x3<double> RGB_YIQ = new
    (
        0.299,  0.587,  0.114,
        0.596, -0.275, -0.321,
        0.212, -0.528,  0.311
    );

    public static readonly Matrix3x3<double> YIQ_RGB = new
    (
        1,  0.956,  0.621,
        1, -0.272, -0.647,
        1, -1.108,  1.705
    );

    public YIQ() : this(default, default, default) { }

    public YIQ(double yiq) : this(yiq, yiq, yiq) { }

    public YIQ(IVector3<double> yiq) : this(yiq.X, yiq.Y, yiq.Z) { }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="YIQ"/></summary>
    public override void From(in Lrgb i, ColorProfile profile)
    {
        double r = i.X, g = i.Y, b = i.Z;

        var Y = (r * RGB_YIQ[0, 0]) + (g * RGB_YIQ[0, 1]) + (b * RGB_YIQ[0, 2]);
        double I = 0, Q = 0;

        if (r != g || g != b)
        {
            I = (r * RGB_YIQ[1, 0]) + (g * RGB_YIQ[1, 1]) + (b * RGB_YIQ[1, 2]);
            Q = (r * RGB_YIQ[2, 0]) + (g * RGB_YIQ[2, 1]) + (b * RGB_YIQ[2, 2]);
        }

        XYZ = new(Y, I, Q);
    }

    /// <summary><see cref="YIQ"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
        => YIQ_RGB.Do(Operator.Multiply, this).Clamp(Lrgb.MinValue, Lrgb.MaxValue);
}