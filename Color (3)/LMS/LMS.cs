using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Long (L/ρ), Medium (M/γ), Short (S/β)</b>
/// <para>A model that defines color based on the response of the three types of cones of the human eye, named for their responsivity (sensitivity) peaks at long, medium, and short wavelengths.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="LMS"/></para>
/// </summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
[ColorOf<XYZ>]
[Component(1, '%', "L", "Long")]
[Component(1, '%', "M", "Medium")]
[Component(1, '%', "S", "Short")]
[Description("A model that defines color based on the response of the three types of cones of the human eye, named for their responsivity (sensitivity) peaks at long, medium, and short wavelengths.")]
public record class LMS(double L, double M, double S)
    : Color3<LMS, double, XYZ>(L, M, S), IColor3<LMS, double>, System.Numerics.IMinMaxValue<LMS>
{
    public static LMS MaxValue => new(1);

    public static LMS MinValue => new(0);

    public LMS() : this(default, default, default) { }

    public LMS(double lms) : this(lms, lms, lms) { }

    public LMS(IVector3<double> lms) : this(lms.X, lms.Y, lms.Z) { }

    /// <summary><see cref="LMS"/> (0) > <see cref="LMS"/> (1)</summary>
    public override void Adapt(ColorProfile source, ColorProfile target) => XYZ = Adapt(this, source, target);

    /// <summary><see cref="XYZ"/> ⇒ <see cref="LMS"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
        => XYZ = new Vector3<double>(profile.Adaptation * input.XYZ);

    /// <summary><see cref="LMS"/> ⇒ <see cref="XYZ"/></summary>
    public override void To(out XYZ result, ColorProfile profile)
    {
        var v = (profile.Adaptation as IMatrix3x3<double>).Invert() * XYZ;
        result = IColor.New<XYZ>(v);
    }
}