using Ion.Numeral;
using System;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <para><b>Intensity (I), Cyan/red (P), Blue/yellow (T)</b></para>
/// <para>A preceder to 'ICtCp' that is similar to 'YCwCm', but has smoother transitions between hues. 'P' stands for protanopia (or red-green colorblindness) and 'T' stands for tritanopia (another form of colorblindness).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="LMS"/> ⇒ <see cref="IPT"/></para>
/// 
/// <i>Author</i>
/// <list type="bullet">
/// <item>Ebner/Fairchild (1998)</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/tommyettinger/colorful-gdx</remarks>
[ColorOf<LMS>]
[Component(1, '%', "I", "Intensity")]
[Component(-1, 1, '%', "P", "Cyan/red")]
[Component(-1, 1, '%', "T", "Blue/yellow")]
[Description("A preceder to 'ICtCp' that is similar to 'YCwCm', but has smoother transitions between hues. 'P' stands for protanopia (or red-green colorblindness) and 'T' stands for tritanopia (another form of colorblindness).")]
[NotComplete]
public record class IPT(double H, double S, double B)
    : Color3<IPT, double>(H, S, B), IColor3<IPT, double>, System.Numerics.IMinMaxValue<IPT>
{
    public static readonly Matrix3x3<double> M = new
    (
        0.4000,  0.4000,  0.2000,
        4.4550, -4.8510,  0.3960,
        0.8056,  0.3572, -1.1628
    );

    public static IPT MaxValue => new(1);

    public static IPT MinValue => new(0, -1, -1);

    public IPT() : this(default, default, default) { }

    public IPT(double ipt) : this(ipt, ipt, ipt) { }

    public IPT(IVector3<double> ipt) : this(ipt.X, ipt.Y, ipt.Z) { }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="IPT"/></summary>
    public override void From(in Lrgb input, ColorProfile profile)
    {
        var lms = new LMS();
        lms.From(input, profile);

        var l = lms.X >= 0 ? Pow(lms.X, 0.43) : -Pow(-lms.X, 0.43);
        var m = lms.Y >= 0 ? Pow(lms.Y, 0.43) : -Pow(-lms.Y, 0.43);
        var s = lms.Z >= 0 ? Pow(lms.Z, 0.43) : -Pow(-lms.Z, 0.43);

        XYZ = new Vector3<double>(M * new Vector3(l, m, s));
    }

    /// <summary><see cref="IPT"/> ⇒ <see cref="Lrgb"/></summary>
    [NotComplete]
    public override Lrgb To(ColorProfile profile)
    {
        //(1) IPT > LMS
        var m = (M as IMatrix3x3<double>).Invert() * XYZ;

        var lms = new LMS(m);

        //(2) ?

        //(3) LMS > Lrgb
        return lms.To(profile);
    }
}