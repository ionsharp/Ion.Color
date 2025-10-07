using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Perceived lightness (L), Red/green (a), Blue/yellow (b)</b>
/// <para>A model that defines color as having perceived lightness (L), red/green (a), and blue/yellow (b).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Labk"/></para>
/// </summary>
/// <remarks>https://colour.readthedocs.io/en/develop/_modules/colour/models/oklab.html</remarks>
[ColorOf<XYZ>]
[Component(1, '°', "L", "Perceived lightness")]
[Component(1, '%', "a", "Red/green")]
[Component(1, '%', "b", "Blue/yellow")]
[ComponentGroup(ComponentGroup.Lightness | ComponentGroup.AB)]
[Description("A model that defines color as having perceived lightness (L), red/green (a), and blue/yellow (b).")]
[Hide]
public record class Labk(double L, double A, double B)
    : Color3<Labk, double, XYZ>(L, A, B), IColor3<Labk, double>, System.Numerics.IMinMaxValue<Labk>
{
    public static Labk MaxValue => new(1);

    public static Labk MinValue => new(0);

    public Labk() : this(default, default, default) { }

    public Labk(double lab) : this(lab, lab, lab) { }

    public Labk(IVector3<double> lab) : this(lab.X, lab.Y, lab.Z) { }

    public static Matrix3x3<double> XYZ_LMS => new
    (
        0.8189330101, 0.3618667424, -0.1288597137,
        0.0329845436, 0.9293118715, 0.0361456387,
        0.0482003018, 0.2643662691, 0.6338517070
    );
    public static Matrix3x3<double> LMS_XYZ => (XYZ_LMS as IMatrix3x3<double>).Invert();

    public static Matrix3x3<double> LMS_LAB => new
    (
        0.2104542553, 0.7936177850, -0.0040720468,
        1.9779984951, -2.4285922050, 0.4505937099,
        0.0259040371, 0.7827717662, -0.8086757660
    );
    public static Matrix3x3<double> LAB_LMS => (LMS_LAB as IMatrix3x3<double>).Invert();

    /// <summary><see cref="XYZ"/> ⇒ <see cref="Labk"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
    {
        var v = input.XYZ.Do(Operator.Divide, (XYZ)(xyY)(XY)profile.Chromacity);
        var q = IColor.New<XYZ>(v.X, v.Y, v.Z);

        var lms = XYZ_LMS * q.XYZ;
        var lmsPrime = lms.Root3();

        var lab = LMS_LAB * lmsPrime;
        XYZ = new(lab);
    }

    /// <summary><see cref="Labk"/> ⇒ <see cref="XYZ"/></summary>
    public override void To(out XYZ result, ColorProfile profile)
    {
        var lab = XYZ;

        var lms = LAB_LMS * lab;
        var lmsPrime = lms.Pow3();

        var xyz = LMS_XYZ * lmsPrime;
        xyz = new Vector3<double>(profile.Chromacity * xyz);

        result = IColor.New<XYZ>(xyz);
    }
}