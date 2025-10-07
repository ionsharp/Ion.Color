using Ion.Numeral;
using System;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <b>Cyan (C), Magenta (M), Yellow (Y), White (W)</b>
/// <para>A subtractive model where the secondary colors are added with white.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="CMYW"/></para>
/// </summary>
[ColorOf<Lrgb>]
[Component(100, "C", "Cyan")]
[Component(100, "M", "Magenta")]
[Component(100, "Y", "Yellow")]
[Component(100, "W", "White")]
[ComponentGroup(ComponentGroup.CMY)]
[Description("A subtractive model where the secondary colors are added with white.")]
public sealed record class CMYW(double C, double M, double Y, double W)
    : Color4<CMYW, double>(C, M, Y, W), IColor4<CMYW, double>, System.Numerics.IMinMaxValue<CMYW>
{
    public static CMYW MaxValue => new(100);

    public static CMYW MinValue => new(0);

    public CMYW() : this(default, default, default, default) { }

    public CMYW(double cmyw) : this(cmyw, cmyw, cmyw, cmyw) { }

    public CMYW(double c, double m, double y) : this(c, m, y, default) { }

    public CMYW(in IVector3<double> cmy) : this(cmy.X, cmy.Y, cmy.Z, default) { }

    public CMYW(in IVector4<double> cmyw) : this(cmyw.X, cmyw.Y, cmyw.Z, cmyw.W) { }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="CMYW"/></summary>
    public override void From(in Lrgb i, ColorProfile profile)
    {
        var r = i.X; var g = i.Y; var b = i.Z;
        var w = Min(r, Min(g, b));

        r -= w; r /= 1 - w;
        g -= w; g /= 1 - w;
        b -= w; b /= 1 - w;

        XYZW = new Vector4<Double1>(1 - r, 1 - g, 1 - b, w).Denormalize(MinValue, MaxValue);
    }

    /// <summary><see cref="CMYW"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        var result = XYZW.New<Vector4<double>, double>(i => 100 - i).Normalize(MinValue, MaxValue);
        Double1 r =  result.X, g = result.Y, b = result.Z, w = result.W;
        r *= (1 - w); r += w;
        g *= (1 - w); g += w;
        b *= (1 - w); b += w;
        return new(r, g, b);
    }
}