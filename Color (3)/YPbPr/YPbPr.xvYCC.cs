using Ion.Numeral;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Ion.Colors;

/// <summary>
/// <b>x (Y), v (Cb), Y (Cr)</b>
/// <para>A color model that can be used in television sets to support a gamut 1.8 times as large as that of the sRGB color space.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="YPbPr"/> ⇒ <see cref="xvYCC"/></para>
/// 
/// <i>Author</i>
/// <list type="bullet">
/// <item>Sony</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/xvycc.js</remarks>
[ColorOf<YPbPr>]
[Component(255, ' ', "x", "Y")]
[Component(255, ' ', "v", "Cb")]
[Component(255, ' ', "Y", "Cr")]
[ComponentGroup(ComponentGroup.Luminance)]
[Description("A color model that can be used in television sets to support a gamut 1.8 times as large as that of the sRGB color space.")]
[SuppressMessage("Style", "IDE1006:Naming Styles")]
public record class xvYCC(double Y, double Cb, double Cr)
    : Color3<xvYCC, double, YPbPr>(Y, Cb, Cr), IColor3<xvYCC, double>, System.Numerics.IMinMaxValue<xvYCC>
{
    public static xvYCC MaxValue => new(255);

    public static xvYCC MinValue => new(0);

    public xvYCC() : this(default, default, default) { }

    public xvYCC(double ycbcr) : this(ycbcr, ycbcr, ycbcr) { }

    public xvYCC(IVector3<double> ycbcr) : this(ycbcr.X, ycbcr.Y, ycbcr.Z) { }

    /// <summary><see cref="YPbPr"/> ⇒ <see cref="xvYCC"/></summary>
    public override void From(in YPbPr input, ColorProfile profile)
    {
        double y = input.X, pb = input.Y, pr = input.Z;
        XYZ = new(16 + 219 * y, 128 + 224 * pb, 128 + 224 * pr);
    }

    /// <summary><see cref="xvYCC"/> ⇒ <see cref="YPbPr"/></summary>
    public override void To(out YPbPr result, ColorProfile profile)
    {
        double y = X, cb = Y, cr = Z;
        result = IColor.New<YPbPr>((y - 16) / 219, (cb - 128) / 224, (cr - 128) / 224);
    }
}