using Ion.Numeral;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Ion.Colors;

/// <summary>
/// <b>Chroma (x), Chroma (y), Luminance (Y)</b>
/// <para>A model that defines color as having chroma (x), chroma (y), and luminance (Y).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="xyY"/></para>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/xyy.js</remarks>
[ColorOf<XYZ>]
[Component(1, "x", "Chroma")]
[Component(1, "y", "Chroma")]
[Component(1, '%', "Y", "Luminance")]
[ComponentGroup(ComponentGroup.Luminance | ComponentGroup.AB)]
[Description("A model that defines color as having chroma (x), chroma (y), and luminance (Y).")]
[SuppressMessage("Style", "IDE1006:Naming Styles")]
public record class xyY(double X, double Y1, double Y2)
    : Color3<xyY, double, XYZ>(X, Y1, Y2), IColor3<xyY, double>, System.Numerics.IMinMaxValue<xyY>
{
    public static xyY MaxValue => new(1);

    public static xyY MinValue => new(0);

    public xyY() : this(default, default, default) { }

    public xyY(double xyy) : this(xyy, xyy, xyy) { }

    public xyY(double x, double y) : this(x, y, MaxValue.Z) { }

    public xyY(in IVector2<double> xy) : this(xy.X, xy.Y, MaxValue.Z) { }

    public xyY(in IVector3<double> xyy) : this(xyy.X, xyy.Y, xyy.Z) { }

    public static explicit operator xyY(in XY input) => IColor.New<xyY>(input.X, input.Y, 1);

    public static explicit operator xyY(in XYZ input)
    {
        var result = new xyY();
        result.From(input, ColorProfile.Default);
        return result;
    }

    /// <summary><see cref="XYZ"/> ⇒ <see cref="xyY"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
        => XYZ = input.XYZ.GetSum() is double sum && sum == 0 ? new Vector3<double>(0, 0, input.Y) : new(input.X / sum, input.Y / sum, input.Y);

    /// <summary><see cref="xyY"/> ⇒ <see cref="XYZ"/></summary>
    public override void To(out XYZ result, ColorProfile profile)
        => result = Y == 0 ? IColor.New<XYZ>(0) : IColor.New<XYZ>(X * Z / Y, Z, (1 - X - Y) * Z / Y);
}