using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <para><b>Luminance (Y), Co, Cg</b></para>
/// <para>A model that defines color as having luminance (Y), 'Chrominance green' (Cg), and 'Chrominance orange' (Co).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="YCoCg"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>YCgCo</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/ycgco.js</remarks>
[ColorOf<Lrgb>]
[Component(1, '%', "Y", "Luminance")]
[Component(-0.5, 0.5, ' ', "Co")]
[Component(-0.5, 0.5, ' ', "Cg")]
[ComponentGroup(ComponentGroup.Luminance)]
[Description("A model that defines color as having luminance (Y), 'Chrominance green' (Cg), and 'Chrominance orange' (Co).")]
public record class YCoCg(double Y, double Co, double Cg)
    : Color3<YCoCg, double>(Y, Co, Cg), IColor3<YCoCg, double>, System.Numerics.IMinMaxValue<YCoCg>
{
    public static YCoCg MaxValue => new(1, +0.5, +0.5);

    public static YCoCg MinValue => new(0, -0.5, -0.5);

    public static readonly Matrix3x3<double> RGB_YCoCg = new
    (
         0.25, 0.5,  0.25,
        -0.25, 0.5, -0.25,
         0.50, 0.0, -0.50
    );

    public YCoCg() : this(default, default, default) { }

    public YCoCg(double ycocg) : this(ycocg, ycocg, ycocg) { }

    public YCoCg(IVector3<double> ycocg) : this(ycocg.X, ycocg.Y, ycocg.Z) { }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="YCoCg"/></summary>
    public override void From(in Lrgb i, ColorProfile profile)
        => XYZ = RGB_YCoCg.Do(Operator.Multiply, i);

    /// <summary><see cref="YCoCg"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        double y = X, cg = Y, co = Z;

        var c = y - cg;
        return new(c + co, y + cg, c - co);
    }
}