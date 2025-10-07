using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Luminance (Y), E-factor (E), S-factor (S)</b>
/// <para>A model that defines color as having luminance (Y), 'E-factor' (E), and 'S-factor' (S).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="YES"/></para>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/yes.js</remarks>
[ColorOf<Lrgb>]
[Component(1, '%', "Y", "Luminance")]
[Component(1, '%', "E", "E-factor")]
[Component(1, '%', "S", "S-factor")]
[ComponentGroup(ComponentGroup.Luminance)]
[Description("A model that defines color as having luminance (Y), 'E-factor' (E), and 'S-factor' (S).")]
public record class YES(Double1 Y, Double1 E, Double1 S)
    : Color3<YES, Double1>(Y, E, S), IColor3<YES, Double1>, System.Numerics.IMinMaxValue<YES>
{
    public static YES MaxValue => new(Double1.MaxValue);

    public static YES MinValue => new(Double1.MinValue);

    public static readonly Matrix3x3<double> RGB_YES = new
    (
        0.253,  0.684,  0.063,
        0.500, -0.500,  0.000,
        0.250,  0.250, -0.500
    );

    public static readonly Matrix3x3<double> YES_RGB = new
    (
        1.000,  1.431,  0.126,
        1.000, -0.569,  0.126,
        1.000,  0.431, -1.874
    );

    public YES() : this(default, default, default) { }

    public YES(Double1 yes) : this(yes, yes, yes) { }

    public YES(IVector3<Double1> yes) : this(yes.X, yes.Y, yes.Z) { }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="YES"/></summary>
    public override void From(in Lrgb i, ColorProfile profile)
        => XYZ = new(RGB_YES.Do(Operator.Multiply, i.XYZ).NewType(j => new Double1(j)));

    /// <summary><see cref="YES"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
        => YES_RGB.Do(Operator.Multiply, this.NewType(i => i.ToDouble()));
}