using Ion.Numeral;
using System;
using static Ion.Numeral.Number;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <b>Hue (H), Saturation (S), Mixture (M)</b>
/// <para>A model that defines color as having hue (H), saturation (S), and mixture (M).</para>
/// <para>🞩 <i>Only one hue seemingly displayed.</i></para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="HSM"/></para>
/// </summary>
/// <remarks>https://seer.ufrgs.br/rita/article/viewFile/rita_v16_n2_p141/7428</remarks>
[ColorOf<Lrgb>]
[Component(360, '°', "H", "Hue")]
[Component(100, '%', "S", "Saturation")]
[Component(255, ' ', "M", "Mixture")]
[ComponentGroup(ComponentGroup.H | ComponentGroup.HS)]
[Description("A model that defines color as having hue (H), saturation (S), and mixture (M).")]
public record class HSM(double H, double S, double M)
    : Color3<HSM, double>(H, S, M), IColor3<HSM, double>, System.Numerics.IMinMaxValue<HSM>
{
    public static HSM MaxValue => new(360, 100, 255);

    public static HSM MinValue => new(0);

    public HSM() : this(default, default, default) { }

    public HSM(double hsm) : this(hsm, hsm, hsm) { }

    public HSM(IVector3<double> hsm) : this(hsm.X, hsm.Y, hsm.Z) { }

    private static bool aB<T>(T i, T a, T b) where T : System.Numerics.INumber<T> => i > a && i <= b;

    /// <summary><see cref="HSM"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        var max = (Vector3)MaxValue;

        double h = Cos(new Angle(X).Convert(AngleType.Radian)), s = Y / max.Y /*(100)*/, m = Z / max.Z /*(255)*/;
        double r, g, b;

        double i = h * s;
        double j = i * Sqrt(41);

        double x = 4 / 861;
        double y = 861 * (s).Pow2();
        double z = 1 - (h).Pow2();

        r = (3 / 41 * i) + m - (x * Sqrt(y * z));
        g = (j + (23 * m) - (19 * r)) / 4;
        b = ((11 * r) - (9 * m) - j) / 2;

        return IColor.New<Lrgb>(r, g, b);
    }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="HSM"/></summary>
    public override void From(in Lrgb input, ColorProfile profile)
    {
        var m = ((4 * input.X) + (2 * input.Y) + input.Z) / 7;

        double t, w;

        var j = (3 * (input.X - m) - 4 * (input.Y - m) - 4 * (input.Z - m)) / Sqrt(41);
        var k = Sqrt((input.X - m).Pow2() + (input.Y - m).Pow2() + (input.Z - m).Pow2());

        t = Acos(j / k);
        w = input.Z <= input.Y ? t : PI * 2 - t;

        double r = input.X, g = input.Y, b = input.Z;

        double u, v = 0;
        u = (r - m).Pow2() + (g - m).Pow2() + (b - m).Pow2();

        if (m >= 0 / 7 && m <= 1 / 7)
        {
            v = (0 - m).Pow2() + (0 - m).Pow2() + (7 - m).Pow2();
        }
        else if (aB(m, 1 / 7, 3 / 7))
        {
            v = (0 - m).Pow2() + (((7 * m - 1) / 2) - m).Pow2() + (1 - m).Pow2();
        }
        else if (aB(m, 3 / 7, 1 / 2))
        {
            v = (((7 * m - 3) / 2) - m).Pow2() + (1 - m).Pow2() + (1 - m).Pow2();
        }
        else if (aB(m, 1 / 2, 4 / 7))
        {
            v = (((7 * m) / 4) - m).Pow2() + (0 - m).Pow2() + (0 - m).Pow2();
        }
        else if (aB(m, 4 / 7, 6 / 7))
        {
            v = (1 - m).Pow2() + (((7 * m - 4) / 2) - m).Pow2() + (0 - m).Pow2();
        }
        else if (aB(m, 6 / 7, 7 / 7))
        {
            v = (1 - m).Pow2() + (1 - m).Pow2() + ((7 * m - 6) - m).Pow2();
        }

        double h = w / PI * 2;
        double s = Sqrt(u) / Sqrt(v);

        var max = (Vector3)MaxValue;
        XYZ = new(h * max.X, s * max.Y, m * max.Z);
    }
}