using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <para><b>X, Y, Z</b></para>
/// A color model based on 'LMS' where 'Z' corresponds to the short (S) cone response of the human eye, 'Y' is a mix of long (L) and medium (M) cone responses, and 'X' is a mix of all three.
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>CIEXYZ</item>
/// </list>
/// 
/// <i>Author</i>
/// <list type="bullet">
/// <item><see cref="CIE"/> (1931)</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
[ColorOf<Lrgb>]
[Component(1, "X")]
[Component(1, "Y")]
[Component(1, "Z")]
[Description("A color model based on 'LMS' where 'Z' corresponds to the short (S) cone response of the human eye, 'Y' is a mix of long (L) and medium (M) cone responses, and 'X' is a mix of all three.")]
public record class XYZ(double X, double Y, double Z)
    : Color3<XYZ, double>(X, Y, Z), IColor3<XYZ, double>, System.Numerics.IMinMaxValue<XYZ>
{
    public static XYZ MaxValue => new(1);

    public static XYZ MinValue => new(0);

    public XYZ() : this(default, default, default) { }

    public XYZ(double xyz) : this(xyz, xyz, xyz) { }

    public XYZ(IVector3<double> xyz) : this(xyz.X, xyz.Y, xyz.Z) { }

    public static explicit operator XYZ(XY input) => (XYZ)(xyY)input;

    public static explicit operator XYZ(xyY input)
    {
        input.To(out XYZ result, default);
        return result;
    }

    public static implicit operator Vector3(in XYZ i) => new(i);

    public static implicit operator Vector3<Double>(in XYZ i) => new(i);

    public static implicit operator XYZ(in Vector3 i) => new(i as IVector3<double>);

    public static implicit operator XYZ(in Vector3<Double> i) => new(i as IVector3<double>);

    /// <summary><see cref="XYZ"/> (0) > <see cref="LMS"/> (0) > <see cref="LMS"/> (1) > <see cref="XYZ"/> (1)</summary>
    public override void Adapt(ColorProfile source, ColorProfile target) => XYZ = Adapt(this, source, target);

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="XYZ"/></summary>
    public override void From(in Lrgb i, ColorProfile profile)
        => XYZ = new ChromacityMatrix(profile) * i;

    /// <summary><see cref="XYZ"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
        => (new ChromacityMatrix(profile) as IMatrix3x3<double>).Invert() * this;
}