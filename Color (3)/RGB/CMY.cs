using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Cyan (C), Magenta (M), Yellow (Y)</b>
/// <para>A subtractive model where the secondary colors are added together.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="CMY"/></para>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/cmy.js</remarks>
[ColorOf<Lrgb>]
[Component(100, "C", "Cyan")]
[Component(100, "M", "Magenta")]
[Component(100, "Y", "Yellow")]
[ComponentGroup(ComponentGroup.CMY)]
[Description("A subtractive model where the secondary colors are added together.")]
public record class CMY(double C, double M, double Y)
    : Color3<CMY, double>(C, M, Y), IColor3<CMY, double>, System.Numerics.IMinMaxValue<CMY>
{
    public static CMY MaxValue => new(100);

    public static CMY MinValue => new(0);

    public CMY() : this(default, default, default) { }

    public CMY(double cmy) : this(cmy, cmy, cmy) { }

    public CMY(IVector3<double> cmy) : this(cmy.X, cmy.Y, cmy.Z) { }

    /// <summary><see cref="CMY"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
        => IColor.New<Lrgb>(1 - X / 100, 1 - Y / 100, 1 - Z / 100);

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="CMY"/></summary>
    public override void From(in Lrgb input, ColorProfile profile)
        => XYZ = new Vector3(1 - input.X, 1 - input.Y, 1 - input.Z) * 100;
}