using Ion.Numeral;
using System;

using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <para><b>Lightness (L), Red/green (a), Yellow/blue (b)</b></para>
/// <para>A model that defines color as having lightness (L), red/green (a), and yellow/blue (b).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Labj"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>Jzazbz</item>
/// </list>
/// </summary>
/// <remarks>https://observablehq.com/@jrus/jzazbz</remarks>
[ColorOf<XYZ>]
[Component(.0, 1, '%', "L", "Lightness")]
[Component(-1, 1, '%', "a", "Red/green")]
[Component(-1, 1, '%', "b", "Yellow/blue")]
[ComponentGroup(ComponentGroup.Lightness | ComponentGroup.AB)]
[Description("A model that defines color as having lightness (L), red/green (a), and yellow/blue (b).")]
public record class Labj(double L, double A, double B)
    : Color3<Labj, double, XYZ>(L, A, B), IColor3<Labj, double>, System.Numerics.IMinMaxValue<Labj>
{
    public static Labj MaxValue => new(1);

    public static Labj MinValue => new(0, -1, -1);

    public Labj() : this(default, default, default) { }

    public Labj(double lab) : this(lab, lab, lab) { }

    public Labj(IVector3<double> lab) : this(lab.X, lab.Y, lab.Z) { }

    static double PerceptualQuantizer(double x)
    {
        var xx = Pow(x * 1e-4, 0.1593017578125);
        var result = Pow((0.8359375 + 18.8515625 * xx) / (1 + 18.6875 * xx), y: 134.034375);
        return result;
    }

    static double PerceptualQuantizerInverse(double X)
    {
        var XX = Pow(X, 7.460772656268214e-03);
        var result = 1e4 * Pow((0.8359375 - XX) / (18.6875 * XX - 18.8515625), y: 6.277394636015326);
        return result;
    }

    /// <summary><see cref="XYZ"/> ⇒ <see cref="Labj"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
    {
        var X = input.X * 10000d; var Y = input.Y * 10000d; var Z = input.Z * 10000d;

        var Lp = PerceptualQuantizer(0.674207838 * X + 0.382799340 * Y - 0.047570458 * Z);
        var Mp = PerceptualQuantizer(0.149284160 * X + 0.739628340 * Y + 0.083327300 * Z);
        var Sp = PerceptualQuantizer(0.070941080 * X + 0.174768000 * Y + 0.670970020 * Z);

        var Iz = 0.5 * (Lp + Mp);

        var az = 3.524000 * Lp - 4.066708 * Mp + 0.542708 * Sp;
        var bz = 0.199076 * Lp + 1.096799 * Mp - 1.295875 * Sp;
        var Jz = 0.44 * Iz / (1 - 0.56 * Iz) - 1.6295499532821566e-11;

        XYZ = new(Jz, az, bz);
    }

    /// <summary><see cref="Labj"/> ⇒ <see cref="XYZ"/></summary>
    public override void To(out XYZ result, ColorProfile profile)
    {
        var Jz = XYZ.X; var az = XYZ.Y; var bz = XYZ.Z;

        Jz += 1.6295499532821566e-11;
        var Iz = Jz / (0.44 + 0.56 * Jz);

        var L = PerceptualQuantizerInverse(Iz + 1.386050432715393e-1 * az + 5.804731615611869e-2 * bz);
        var M = PerceptualQuantizerInverse(Iz - 1.386050432715393e-1 * az - 5.804731615611891e-2 * bz);
        var S = PerceptualQuantizerInverse(Iz - 9.601924202631895e-2 * az - 8.118918960560390e-1 * bz);

        var X = +1.661373055774069e+00 * L - 9.145230923250668e-01 * M + 2.313620767186147e-01 * S;
        var Y = -3.250758740427037e-01 * L + 1.571847038366936e+00 * M - 2.182538318672940e-01 * S;
        var Z = -9.098281098284756e-02 * L - 3.127282905230740e-01 * M + 1.522766561305260e+00 * S;

        result = IColor.New<XYZ>(X / 10000d, Y / 10000d, Z / 10000d);
    }
}