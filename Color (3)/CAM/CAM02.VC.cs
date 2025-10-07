using Ion.Numeral;
using System;
using static System.Math;

namespace Ion.Colors;

#region (enum) Correlates

/// <summary>Dimensions of color appearance as defined by <see cref="CIE"/>.</summary>
public enum Correlates
{
    /// <summary><b>Brightness</b> (Q)
    /// <para>The subjective appearance of how bright an object appears given its surroundings and how it is illuminated (also called <b>Luminance</b>).</para>
    /// </summary>
    /// <remarks><b>Q = (4 / c) * Sqrt(1 / 100 * J) * (A[w] + 4) * Pow(F[L], 1 / 4)</b></remarks>
    Brightness,
    /// <summary><b>Lightness</b> (J) 
    /// <para>The subjective appearance of how light a color appears to be.</para>
    /// </summary>
    /// <remarks><b>J = 100 * Pow(A / A[w], cz)</b></remarks>
    Lightness,

    /// <summary><b>Chroma</b> (C) 
    /// <para>The colorfulness relative to the brightness of another color that appears white under similar viewing conditions.</para></summary>
    /// <remarks><b>C = Pow(t, 0.9) * Sqrt(1 / 100 * J) * Pow(1.64 - Pow(0.29, n), 0.73)</b></remarks>
    Chroma,
    /// <summary><b>Saturation</b> (s) 
    /// <para>The colorfulness of a color relative to its own brightness.</para>
    /// </summary>
    /// <remarks><b>s = 100 * Sqrt(M / Q)</b></remarks>
    Saturation,

    /// <summary><b>Colorfulness</b> (M)
    /// <para>The degree of difference between a color and gray.</para></summary>
    /// <remarks><b>M = C * Pow(F[L], 1 / 4)</b></remarks>
    Colorfulness,
    /// <summary><b>Hue</b> (h) 
    /// <para>The degree to which a stimulus can be described as similar to or different from stimuli that are described as red, green, blue, and yellow (the so-called unique hues).</para></summary>
    /// <remarks><b>h = </b></remarks>
    Hue,
}

#endregion

#region (enum) Surrounds

/// <summary><see cref="CAM02"/> surround(ing)s as defined by <see cref="CIE"/>.</summary>
public enum Surrounds
{
    /// <summary>
    /// <b>Viewing surface colors.</b>
    /// <para>Relative luminance > 20% of scene white.</para>
    /// </summary>
    /// <remarks><see cref="ViewingConditions.F"/> = 1, <see cref="ViewingConditions.c"/> = 0.690, <see cref="ViewingConditions.Nc"/> = 1</remarks>
    Average,
    /// <summary>
    /// <b>Viewing television.</b>
    /// <para>Relative luminance 0% of scene white.</para>
    /// </summary>
    /// <remarks><see cref="ViewingConditions.F"/> = 0.9, <see cref="ViewingConditions.c"/> = 0.590, <see cref="ViewingConditions.Nc"/> = 0.95</remarks>
    /// <summary></summary>
    Dim,
    /// <summary>
    /// <b>Using a projector in a dark room.</b>
    /// <para>Relative luminance between 0% and 20% of scene white.</para>
    /// </summary>
    /// <remarks><see cref="ViewingConditions.F"/> = 0.8, <see cref="ViewingConditions.c"/> = 0.525, <see cref="ViewingConditions.Nc"/> = 0.8</remarks>
    /// <summary></summary>
    Dark,
}

#endregion

/// <summary><see cref="CAM02"/> viewing conditions.</summary>
public readonly record struct CAM02Conditions
{
    public const double DefaultAbsoluteLuminance = 4;   //4 cd/m^2 (ambient illumination of 64 lux)

    public const double DefaultRelativeLuminance = 20;  //20% gray

    ///

    public readonly Surrounds Surround { get; } = Surrounds.Average;

    public readonly double F = 1;          //Average

    public readonly double c = 0.690;      //Average

    public readonly double Nc = 1;         //Average

    ///

    /// <summary>Achromatic response to white.</summary>
    public readonly double Aw = 0;

    /// <summary>Degree of adaptation (discounting)</summary>
    public readonly double D = 0;

    /// <summary>Luminance level adaptation factor</summary>
    public readonly double FL = 0;

    public readonly double n = 0, Nbb = 0, Ncb = 0, z = 0;

    [Name("Absolute luminance of adapting field")]
    public double LA { get; } = DefaultAbsoluteLuminance;

    [Name("Relative luminance of background")]
    public double Yb { get; } = DefaultRelativeLuminance;

    ///

    public CAM02Conditions() : this((XYZ)(xyY)(XY)ColorProfile.DefaultWhite) { }

    public CAM02Conditions(Vector3 white) : this(Surrounds.Average, DefaultAbsoluteLuminance, DefaultRelativeLuminance, white) { }

    public CAM02Conditions(Surrounds input, double LA, double Yb, Vector3 white)
    {
        Surround = input;
        switch (Surround)
        {
            case Surrounds.Average:
                F = 1.000; c = 0.690; Nc = 1.000;
                break;
            case Surrounds.Dim:
                F = 0.900; c = 0.590; Nc = 0.900;
                break;
            case Surrounds.Dark:
                F = 0.800; c = 0.525; Nc = 0.800;
                break;
        }

        this.LA = LA; this.Yb = Yb;

        n = Compute_n(Yb, white.Y); z = Compute_z(n);
        Nbb = Compute_Nbb(n); Ncb = Nbb;

        D = Compute_D(F, LA); FL = Compute_FL(LA);

        Aw = Compute_Aw(white, D, FL, Nbb);
    }

    ///

    static double Compute_Aw(Vector3 white, double D, double FL, double Nbb)
    {
        double rc, gc, bc;
        double rpa, gpa, bpa;

        var rgb = ChromacityAdaptationTransform.CAT02 * new Vector3(white.X * 100, white.Y * 100, white.Z * 100);
        var r = rgb.X;
        var g = rgb.Y;
        var b = rgb.Z;

        rc = r * (((white.Y * 100 * D) / r) + (1.0 - D));
        gc = g * (((white.Y * 100 * D) / g) + (1.0 - D));
        bc = b * (((white.Y * 100 * D) / b) + (1.0 - D));

        var rgbp = CAM02.CAT02_HPE * new Vector3(rc, gc, bc);
        var rp = rgbp.X;
        var gp = rgbp.Y;
        var bp = rgbp.Z;

        rpa = CAM02.NonlinearAdaptation(rp, FL);
        gpa = CAM02.NonlinearAdaptation(gp, FL);
        bpa = CAM02.NonlinearAdaptation(bp, FL);

        return ((2.0 * rpa) + gpa + ((1.0 / 20.0) * bpa) - 0.305) * Nbb;
    }

    /// <summary>
    /// Theoretically, <b>D</b> ranges from
    /// 
    /// <para>0 = <b>No adaptation to the adopted white point.</b></para>
    /// <para>1 = <b>Complete adaptation to the adopted white point.</b></para>
    /// 
    /// <para>In practice, the minimum <b>D</b> value will not be less than 0.65 for <see cref="Surrounds.Dark"/> and exponentially converges to 1 for <see cref="Surrounds.Average"/> with increasingly large values of L[A].</para>
    /// 
    /// <para>L[A] is the luminance of the adapting field in cd/m^2.</para>
    /// </summary>
    static double Compute_D(double F, double LA)
        => (F * (1.0 - ((1.0 / 3.6) * Exp((-LA - 42.0) / 92.0))));

    static double Compute_FL(double LA)
    {
        double k, fl;
        k = 1.0 / ((5.0 * LA) + 1.0);
        fl = 0.2 * Pow(k, 4.0) * (5.0 * LA) + 0.1 * (Pow((1.0 - Pow(k, 4.0)), 2.0)) * (Pow((5.0 * LA), (1.0 / 3.0)));
        return (fl);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
    static double Compute_FL_Inverse(double LA)
    {
        double la5 = LA * 5.0;
        double k = 1.0 / (la5 + 1.0);

        k = Pow(k, 4);
        return (0.2 * k * la5) + (0.1 * (1.0 - k) * (1.0 - k) * Pow(la5, 1.0 / 3.0));
    }

    static double Compute_n(double Yb, double Yw) => Yb / Yw * 100;

    static double Compute_Nbb(double n) => 0.725 * Pow(1.0 / n, 0.2);

    static double Compute_z(double n) => 1.48 + Pow(n, 0.5);
}