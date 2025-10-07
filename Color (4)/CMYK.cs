using Ion.Numeral;
using System;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <b>Cyan (C), Magenta (M), Yellow (Y), Black (K)</b>
/// <para>A subtractive model where the secondary colors are added with black.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="CMYK"/></para>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/cmyk.js</remarks>
[ColorOf<Lrgb>]
[Component(100, '%', "C", "Cyan")]
[Component(100, '%', "M", "Magenta")]
[Component(100, '%', "Y", "Yellow")]
[Component(100, '%', "K", "Black")]
[ComponentGroup(ComponentGroup.CMY)]
[Description("A subtractive model where the secondary colors are added with black.")]
public sealed record class CMYK(double C, double M, double Y, double K)
    : Color4<CMYK, double>(C, M, Y, K), IColor4<CMYK, double>, System.Numerics.IMinMaxValue<CMYK>
{
    public static CMYK MaxValue => new(100);

    public static CMYK MinValue => new(0);

    public CMYK() : this(default, default, default, default) { }

    public CMYK(double cmyk) : this(cmyk, cmyk, cmyk, cmyk) { }

    public CMYK(double c, double m, double y) : this(c, m, y, default) { }

    public CMYK(in IVector3<double> cmy) : this(cmy.X, cmy.Y, cmy.Z, default) { }

    public CMYK(in IVector4<double> cmyk) : this(cmyk.X, cmyk.Y, cmyk.Z, cmyk.W) { }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="CMYK"/></summary>
    public override void From(in Lrgb i, ColorProfile profile)
    {
        var k0 = 1.0 - Max(i.X, Max(i.Y, i.Z));
        var k1 = 1.0 - k0;

        var c = (1.0 - i.X - k0) / k1;
        var m = (1.0 - i.Y - k0) / k1;
        var y = (1.0 - i.Z - k0) / k1;

        XYZW = new Vector4<Double1>(c.NaN(0), m.NaN(0), y.NaN(0), k0).Denormalize(MinValue, MaxValue);
    }

    /// <summary><see cref="CMYK"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        var result = XYZW.Normalize(MinValue, MaxValue);
        var r = (1.0 - result.X) * (1.0 - result.W);
        var g = (1.0 - result.Y) * (1.0 - result.W);
        var b = (1.0 - result.Z) * (1.0 - result.W);
        return new(r, g, b);
    }
}