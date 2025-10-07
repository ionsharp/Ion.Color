using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Luminance (Y), Db, Dr</b>
/// <para>The color model used in the SECAM analog terrestrial colour television broadcasting standard and PAL-N.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="YDbDr"/></para>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/ydbdr.js</remarks>
[ColorOf<Lrgb>]
[Component(1, '%', "Y", "Luminance")]
[Component(-1.333, 1.333, ' ', "Db")]
[Component(-1.333, 1.333, ' ', "Dr")]
[ComponentGroup(ComponentGroup.Luminance)]
[Description("The color model used in the SECAM analog terrestrial colour television broadcasting standard and PAL-N.")]
public record class YDbDr(double Y, double Db, double Dr)
    : Color3<YDbDr, double>(Y, Db, Dr), IColor3<YDbDr, double>, System.Numerics.IMinMaxValue<YDbDr>
{
    public static YDbDr MaxValue => new(1, +1.333, +1.333);

    public static YDbDr MinValue => new(0, -1.333, -1.333);

    public static readonly Matrix3x3<double> RGB_YDbDr = new
    (
         0.299,  0.587, 0.114,
        -0.450, -0.883, 1.333,
        -1.333,  1.116, 0.217
    );

    public static readonly Matrix3x3<double> YDbDr_RGB = new
    (
        1,  0.000092303716148, -0.525912630661865,
        1, -0.129132898890509,  0.267899328207599,
        1,  0.664679059978955, -0.000079202543533
    );

    public YDbDr() : this(default, default, default) { }

    public YDbDr(double ydbdr) : this(ydbdr, ydbdr, ydbdr) { }

    public YDbDr(IVector3<double> ydbdr) : this(ydbdr.X, ydbdr.Y, ydbdr.Z) { }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="YDbDr"/></summary>
    public override void From(in Lrgb i, ColorProfile profile)
        => XYZ = RGB_YDbDr.Do(Operator.Multiply, i);

    /// <summary><see cref="YDbDr"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
        => YDbDr_RGB.Do(Operator.Multiply, this);
}