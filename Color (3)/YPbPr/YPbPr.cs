using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Y, Pb, Pr</b>
/// <para>A gamma-corrected color model designed for use in analog systems that is numerically equivalent to 'YCbCr'.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="YPbPr"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>Y′PbPr</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/ypbpr.js</remarks>
[ColorOf<Lrgb>]
[Component(1, '%', "Y", "Luminance")]
[Component(-0.5, 0.5, ' ', "Pb", "")]
[Component(-0.5, 0.5, ' ', "Pr", "")]
[ComponentGroup(ComponentGroup.Luminance)]
[Description("A gamma-corrected color model designed for use in analog systems that is numerically equivalent to 'YCbCr'.")]
public record class YPbPr(double Y, double Pb, double Pr)
    : Color3<YPbPr, double>(Y, Pb, Pr), IColor3<YPbPr, double>, System.Numerics.IMinMaxValue<YPbPr>
{
    public static YPbPr MaxValue => new(1, +0.5, +0.5);

    public static YPbPr MinValue => new(0, -0.5, -0.5);

    public YPbPr() : this(default, default, default) { }

    public YPbPr(double ypbpr) : this(ypbpr, ypbpr, ypbpr) { }

    public YPbPr(IVector3<double> ypbpr) : this(ypbpr.X, ypbpr.Y, ypbpr.Z) { }

    /// <summary><see cref="YPbPr"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        double y = X, pb = Y, pr = Z;

        //ITU-R BT.709
        double kb = 0.0722;
        double kr = 0.2126;

        var r = y + 2 * pr * (1 - kr);
        var b = y + 2 * pb * (1 - kb);
        var g = (y - kr * r - kb * b) / (1 - kr - kb);

        return IColor.New<Lrgb>(r, g, b);
    }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="YPbPr"/></summary>
    public override void From(in Lrgb input, ColorProfile profile)
    {
        double r = input.X, g = input.Y, b = input.Z;

        //ITU-R BT.709
        double kb = 0.0722;
        double kr = 0.2126;

        var y = kr * r + (1 - kr - kb) * g + kb * b;
        var pb = 0.5 * (b - y) / (1 - kb);
        var pr = 0.5 * (r - y) / (1 - kr);

        XYZ = new(y, pb, pr);
    }
}