using Ion.Numeral;
using System;
using static Ion.Numeral.Number;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <para><b>Lightness (L), Yellow/blue (j), Green/red (g)</b></para>
/// <para>A model that attempts to accurately represent uniform color differences in each direction.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Labi"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>osaucs</item>
/// <item>OSA-UCS</item>
/// <item>Optical Society of America Uniform Color Space</item>
/// </list>
/// 
/// <i>Author</i>
/// <list type="bullet">
/// <item>Optical Society of America’s Committee on Uniform Color Scales (1947)</item>
/// </list>
/// </summary>
/// <remarks>
/// <para>https://arxiv.org/pdf/1911.08323v2.pdf</para>
/// <para>https://github.com/colorjs/color-space/blob/master/osaucs.js</para>
/// </remarks>
[ColorOf<XYZ>]
[Component(-10, 8, "L", "Lightness")]
[Component(-6, 12, "j", "Red/green")]
[Component(-10, 6, "g", "Yellow/blue")]
[ComponentGroup(ComponentGroup.Lightness | ComponentGroup.AB)]
[Description("A model that attempts to accurately represent uniform color differences in each direction.")]
[Hide]
public record class Labi(double L, double J, double G)
    : Color3<Labi, double, XYZ>(L, J, G), IColor3<Labi, double>, System.Numerics.IMinMaxValue<Labi>
{
    public static Labi MaxValue => new(8, 12, 6);

    public static Labi MinValue => new(-10, -6, -10);

    public Labi() : this(default, default, default) { }

    public Labi(double ljg) : this(ljg, ljg, ljg) { }

    public Labi(IVector3<double> ljg) : this(ljg.X, ljg.Y, ljg.Z) { }

    /// <summary><see cref="XYZ"/> ⇒ <see cref="Labi"/></summary>
    [NotComplete]
    public override void From(in XYZ input, ColorProfile profile)
    {
        double X = input.X, Y = input.Y, Z = input.Z;

        var x = X / (X + Y + Z);
        var y = Y / (X + Y + Z);

        //FIXME: there might be a typo, wiki states 1.8103 as a constant value
        var K = 4.4934 * x * x + 4.3034 * y * y - 4.276 * x * y - 1.3744 * x - 2.56439 * y + 1.8103;
        var Y0 = K * Y;

        var L_ = 5.9 * (Pow(Y0, 1 / 3) - 2 / 3 + 0.042 * Pow(Max(Y0, 30) - 30, 1 / 3));
        var L = (L_ - 14.3993) / Sqrt(2);

        var C = L_ / (5.9 * (Pow(Y0, 1 / 3) - 2 / 3));

        var R = 0.7790 * X + 0.4194 * Y - 0.1648 * Z;
        var G = -0.4493 * X + 1.3265 * Y + 0.0927 * Z;
        var B = -0.1149 * X + 0.3394 * Y + 0.7170 * Z;

        R = Cbrt(R);
        G = Cbrt(G);
        B = Cbrt(B);

        var a = -13.7 * R + 17.7 * G - 4 * B;
        var b = 1.7 * R + 8 * G - 9.7 * B;

        var g = C * a;
        var j = C * b;

        //Polar form
        //var p   = Sqrt(j * j + g * g);
        //var phi = Atan2(j, g);

        XYZ = new(L, j, g);
    }

    /// <summary><see cref="Labi"/> ⇒ <see cref="XYZ"/></summary>
    [NotComplete]
    public override void To(out XYZ result, ColorProfile profile)
    {
        var L = XYZ.X * Sqrt(2) + 14.3993;
        //0  = f(t) := Pow3((L' / 5.9) + (2 / 3) - t) - Pow3(0.042) * (Pow3(t) - 30)

        //f(t) is a monotonically decreasing cubic polynomial. It has exactly one root that can be found using the classical Cardano formula.

        //[1] Expand
        //	
        //		f(t) = a * Pow3(t) + b * Pow3(t) + c * t + d
        //	
        //	  with:

        var u = (L / 5.9) + (2 / 3);
        var v = 0.042.Pow3();
        double a = -(v + 1), b = 3 * u, c = -3 * u.Pow2(), d = u.Pow3() + 30 * v;

        //[2] Compute the depressed form:
        //
        //		f(t) = a(Pow3(x) + p * x + q

        var p = (3 * a * c - b.Pow2()) / (3 * a.Pow2());
        var q = ((2 * b.Pow3()) - (9 * a * b * c) + (27 * a.Pow2() * d)) / (27 * a.Pow3());

        //[3] Compute the root as
        var t = -(b / 3 * a) + Cbrt(-(q / 2) + Sqrt((q / 2).Pow2() + (p / 3).Pow3())) + Cbrt(-(q / 2) - Sqrt((q / 2).Pow2() + (p / 3).Pow3()));

        //The expression in the square root, (q / 2)^2 + (p / 3)^3 is always + since f(t) has exactly one root.

        _ = t.Pow3();
        var C = L / (5.9 * (t - (2 / 3)));
        _ = XYZ.Z / C;
        _ = XYZ.Y / C;

        //[4] With a0 and b0, "pin down" √3R, √3G, √3B to only "one degree of freedom" (w).
        //
        //(w) will be found by Newton iteration. The function φ(w) of which a root needs to be found is:
        //
        //A = |-13.7, 17.7, -4.0|
        //    |  1.7,  8.0, -9.7|
        //
        //Append matrix (A) with a row such that the new 3x3 matrix (A~) is nonsingular and solve:
        //
        //|a| = A~|√3R|
        //|b|     |√3G|
        //|w|     |√3B|
        //
        //Append [1, 0, 0], which corresponds to setting w = Cbrt(R).
        //Then compute the tentative X~, Y~, Z~ via (3)
        //And get the corresponding tentative Y0~ from (1)
        //Then φ(w) = Y0~(w) - Y0
        //If the difference between Y0~(w) and Y0 from (6) is 0, the correct w has been found.
        double x = 0, y = 0, z = 0;
        result = IColor.New<XYZ>(x, y, z);
    }
}