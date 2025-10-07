using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <para><b>Luminance (Y), Cb, Cr</b></para>
/// <para>A color model used as a part of the color image pipeline in digital systems where Y′ is a luma component, and CB and CR are the blue-difference and red-difference chroma components, respectively.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="YPbPr"/> ⇒ <see cref="YCbCr"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>Y′CbCr</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/ycbcr.js</remarks>
[ColorOf<YPbPr>]
[Component(16, 235, ' ', "Y", "Luminance")]
[Component(16, 240, ' ', "Cb")]
[Component(16, 240, ' ', "Cr")]
[ComponentGroup(ComponentGroup.Luminance)]
[Description("A color model used as a part of the color image pipeline in digital systems where Y′ is a luma component, and CB and CR are the blue-difference and red-difference chroma components, respectively.")]
public record class YCbCr(double Y, double Cb, double Cr)
    : Color3<YCbCr, double, YPbPr>(Y, Cb, Cr), IColor3<YCbCr, double>, System.Numerics.IMinMaxValue<YCbCr>
{
    public static YCbCr MaxValue => new(235, 240, 240);

    public static YCbCr MinValue => new(16);

    public YCbCr() : this(default, default, default) { }

    public YCbCr(double ycbcr) : this(ycbcr, ycbcr, ycbcr) { }

    public YCbCr(IVector3<double> ycbcr) : this(ycbcr.X, ycbcr.Y, ycbcr.Z) { }

    /// <summary><see cref="YPbPr"/> ⇒ <see cref="YCbCr"/></summary>
    public override void From(in YPbPr input, ColorProfile profile)
    {
        double y = input.X, pb = input.Y, pr = input.Z;
        XYZ = new(16 + 219 * y, 128 + 224 * pb, 128 + 224 * pr);
    }

    /// <summary><see cref="YCbCr"/> ⇒ <see cref="YPbPr"/></summary>
    public override void To(out YPbPr result, ColorProfile profile)
    {
        double y = X, cb = Y, cr = Z;
        result = IColor.New<YPbPr>((y - 16) / 219, (cb - 128) / 224, (cr - 128) / 224);
    }
}