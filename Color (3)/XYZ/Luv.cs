using Ion.Numeral;
using System;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <para><b>Lightness (L*), u*, v*</b></para>
/// <para>An Adams chromatic valence color model that attempts perceptual uniformity (successor to 'UVW').</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Luv"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>CIELUV</item>
/// <item>L*u*v*</item>
/// </list>
/// 
/// <i>Author</i>
/// <list type="bullet">
/// <item><see cref="CIE"/> (1976)</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
[ColorOf<XYZ>]
[Component(100, '%', "L*", "Lightness")]
[Component(-134, 224, ' ', "u*")]
[Component(-140, 122, ' ', "v*")]
[ComponentGroup(ComponentGroup.Lightness)]
[Description("An Adams chromatic valence color model that attempts perceptual uniformity (successor to 'UVW').")]
public record class Luv(double L, double U, double V)
    : Color3<Luv, double, XYZ>(L, U, V), IColor3<Luv, double>, System.Numerics.IMinMaxValue<Luv>
{
    public static Luv MaxValue => new(100, +224, +122);

    public static Luv MinValue => new(0, -134, -140);

    public Luv() : this(default, default, default) { }

    public Luv(double luv) : this(luv, luv, luv) { }

    public Luv(IVector3<double> luv) : this(luv.X, luv.Y, luv.Z) { }

    /// <summary><see cref="XYZ"/> ⇒ <see cref="Luv"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
    {
        static double Compute_up(XYZ i) => 4 * i.X / (i.X + 15 * i.Y + 3 * i.Z);
        static double Compute_vp(XYZ i) => 9 * i.Y / (i.X + 15 * i.Y + 3 * i.Z);

        var yr = input.Y / profile.Chromacity.Y;
        var up = Compute_up(input);
        var vp = Compute_vp(input);

        var upr = Compute_up((XYZ)(xyY)(XY)profile.Chromacity);
        var vpr = Compute_vp((XYZ)(xyY)(XY)profile.Chromacity);

        var L = yr > CIE.IEpsilon ? 116 * Pow(yr, 1 / 3d) - 16 : CIE.IKappa * yr;

        if (double.IsNaN(L) || L < 0)
            L = 0;

        var u = 13 * L * (up - upr);
        var v = 13 * L * (vp - vpr);

        if (double.IsNaN(u))
            u = 0;

        if (double.IsNaN(v))
            v = 0;

        XYZ = new(L, u, v);
    }

    /// <summary><see cref="Luv"/> ⇒ <see cref="XYZ"/></summary>
    public override void To(out XYZ result, ColorProfile profile)
    {
        static double Compute_u0(XYZ input) => 4 * input.X / (input.X + 15 * input.Y + 3 * input.Z);
        static double Compute_v0(XYZ input) => 9 * input.Y / (input.X + 15 * input.Y + 3 * input.Z);

        double L = XYZ.X, u = XYZ.Y, v = XYZ.Z;

        var u0 = Compute_u0((XYZ)(xyY)(XY)profile.Chromacity);
        var v0 = Compute_v0((XYZ)(xyY)(XY)profile.Chromacity);

        var Y = L > CIE.IKappa * CIE.IEpsilon
            ? Pow((L + 16) / 116, 3)
            : L / CIE.IKappa;

        var a = (52 * L / (u + 13 * L * u0) - 1) / 3;
        var b = -5 * Y;
        var c = -1 / 3d;
        var d = Y * (39 * L / (v + 13 * L * v0) - 5);

        var X = (d - b) / (a - c);
        var Z = X * a + b;

        if (double.IsNaN(X) || X < 0)
            X = 0;

        if (double.IsNaN(Y) || Y < 0)
            Y = 0;

        if (double.IsNaN(Z) || Z < 0)
            Z = 0;

        result = IColor.New<XYZ>(X, Y, Z);
    }
}