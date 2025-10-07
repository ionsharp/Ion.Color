using Ion.Numeral;
using System;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <para><b>Lightness (L), a, b</b></para>
/// <para>A model that defines color as having lightness (L), chroma (a), and chroma (b).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Labh"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>Hunter Lab</item>
/// </list>
/// 
/// <i>Author</i>
/// <list type="bullet">
/// <item>Richard S. Hunter (1948)</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
[ColorOf<XYZ>]
[Component(100, '%', "L", "Lightness")]
[Component(-100, 100, ' ', "a")]
[Component(-100, 100, ' ', "b")]
[ComponentGroup(ComponentGroup.Lightness | ComponentGroup.AB)]
[Description("A model that defines color as having lightness (L), chroma (a), and chroma (b).")]
public record class Labh(double L, double A, double B)
    : Color3<Labh, double, XYZ>(L, A, B), IColor3<Labh, double>, System.Numerics.IMinMaxValue<Labh>
{
    public static Labh MaxValue => new(100);

    public static Labh MinValue => new(0, -100, -100);

    public Labh() : this(default, default, default) { }

    public Labh(double lab) : this(lab, lab, lab) { }

    public Labh(IVector3<double> lab) : this(lab.X, lab.Y, lab.Z) { }

    /// <summary>Computes the <b>Ka</b> parameter.</summary>
    public static double ComputeKa(Vector3 whitePoint)
    {
        if (whitePoint == (Vector3)(XYZ)(xyY)(XY)Illuminant2.C)
            return 175;

        var Ka = 100 * (175 / 198.04) * (whitePoint.X + whitePoint.Y);
        return Ka;
    }

    /// <summary>Computes the <b>Kb</b> parameter.</summary>
    public static double ComputeKb(Vector3 whitePoint)
    {
        if (whitePoint == (Vector3)(XYZ)(xyY)(XY)Illuminant2.C)
            return 70;

        var Ka = 100 * (70 / 218.11) * (whitePoint.Y + whitePoint.Z);
        return Ka;
    }

    /// <summary><see cref="XYZ"/> ⇒ <see cref="Labh"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
    {
        double X = input.X, Y = input.Y, Z = input.Z;
        double Xn = profile.White.X, Yn = profile.White.Y, Zn = profile.White.Z;

        var Ka = ComputeKa(profile.White);
        var Kb = ComputeKb(profile.White);

        var L = 100 * Sqrt(Y / Yn);
        var a = Ka * ((X / Xn - Y / Yn) / Sqrt(Y / Yn));
        var b = Kb * ((Y / Yn - Z / Zn) / Sqrt(Y / Yn));
        XYZ = new(L, a.NaN(0), b.NaN(0));
    }

    /// <summary><see cref="Labh"/> ⇒ <see cref="XYZ"/></summary>
    public override void To(out XYZ result, ColorProfile profile)
    {
        double L = X, a = Y, b = Z;
        double Xn = profile.White.X, Yn = profile.White.Y, Zn = profile.White.Z;

        var Ka = ComputeKa(profile.White);
        var Kb = ComputeKb(profile.White);

        var y = Pow(L / 100.0, 2) * Yn;
        var x = (a / Ka * Sqrt(y / Yn) + y / Yn) * Xn;
        var z = (b / Kb * Sqrt(y / Yn) - y / Yn) * -Zn;
        result = IColor.New<XYZ>(x, y, z);
    }
}