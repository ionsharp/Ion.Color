using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <para><b>U, C (V), S (W)</b></para>
/// <para>A model that defines color as having chroma (U), chroma (C), and luminance (S).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="UCS"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>CIEUCS</item>
/// <item>Uniform Color Space</item>
/// <item>Uniform Color Scale</item>
/// <item>Uniform Chromaticity Scale</item>
/// <item>Uniform Chromaticity Space</item>
/// </list>
/// 
/// <i>Author</i>
/// <list type="bullet">
/// <item><see cref="CIE"/> (1960)</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/ucs.js</remarks>
[ColorOf<XYZ>]
[Component(1, "U")]
[Component(1, '%', "C", "V")]
[Component(1, '%', "S", "W")]
[ComponentGroup(ComponentGroup.Luminance)]
[Description("A model that defines color as having chroma (U), chroma (C), and luminance (S).")]
public record class UCS(double U, double C, double S)
    : Color3<UCS, double, XYZ>(U, C, S), IColor3<UCS, double>, System.Numerics.IMinMaxValue<UCS>
{
    public static UCS MaxValue => new(1);

    public static UCS MinValue => new(0);

    public UCS() : this(default, default, default) { }

    public UCS(double ucs) : this(ucs, ucs, ucs) { }

    public UCS(IVector3<double> ucs) : this(ucs.X, ucs.Y, ucs.Z) { }

    /// <summary><see cref="XYZ"/> ⇒ <see cref="UCS"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
    {
        double x = input.X, y = input.Y, z = input.Z;
        XYZ = new(x * 2 / 3, y, 0.5 * (-x + 3 * y + z));
    }

    /// <summary><see cref="UCS"/> ⇒ <see cref="XYZ"/></summary>
    public override void To(out XYZ result, ColorProfile profile)
    {
        double u = X, v = Y, w = Z;
        result = IColor.New<XYZ>(1.5 * u, v, 1.5 * u - 3 * v + 2 * w);
    }
}