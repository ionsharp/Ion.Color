using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <para><b>Luminance (Y), U, V</b></para>
/// <para>A model that defines color as having luminance (Y), chroma (U), and chroma (V).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="YUV"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>EBU</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/yuv.js</remarks>
[ColorOf<Lrgb>]
[Component(1, '%', "Y", "Luminance")]
[Component(-0.5, 0.5, ' ', "U")]
[Component(-0.5, 0.5, ' ', "V")]
[ComponentGroup(ComponentGroup.Luminance)]
[Description("A model that defines color as having luminance (Y), chroma (U), and chroma (V).")]
public record class YUV(double Y, double U, double V)
    : Color3<YUV, double>(Y, U, V), IColor3<YUV, double>, System.Numerics.IMinMaxValue<YUV>
{
    public static YUV MaxValue => new(1, +0.5, +0.5);

    public static YUV MinValue => new(0, -0.5, -0.5);

    public static readonly Matrix3x3<double> RGB_YUV = new
    (
         0.29900,  0.58700,  0.11400,
        -0.14713, -0.28886,  0.43600,
         0.61500, -0.51499, -0.10001
    );

    public static readonly Matrix3x3<double> YUV_RGB = new
    (
         1,  0.00000,  1.13983,
         1, -0.39465, -0.58060,
         1,  2.02311,  0.00000
    );

    public YUV() : this(default, default, default) { }

    public YUV(double yuv) : this(yuv, yuv, yuv) { }

    public YUV(IVector3<double> yuv) : this(yuv.X, yuv.Y, yuv.Z) { }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="YUV"/></summary>
    public override void From(in Lrgb i, ColorProfile profile)
        => XYZ = RGB_YUV.Do(Operator.Multiply, i);

    /// <summary><see cref="YUV"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
        => YUV_RGB.Do(Operator.Multiply, this).Clamp(Lrgb.MinValue, Lrgb.MaxValue);
}