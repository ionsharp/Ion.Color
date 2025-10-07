using Ion.Numeral;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <b>xyYC</b> (Coloroid)
/// <para>A color model based on 'xyY'. Also known as 'Coloroid'.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="xyY"/> ⇒ <see cref="xyYC"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>ATV</item>
/// <item>Coloroid</item>
/// </list>
/// </summary>
/// <remarks>
/// <para>https://github.com/colorjs/color-space/blob/master/coloroid.js</para>
/// <para>http://hej.sze.hu/ARC/ARC-030520-A/arc030520a.pdf</para>
/// </remarks>
[ColorOf<xyY>]
[Component(MinHue, MaxHue, "A", "Hue")]
[Component(100, "T")]
[Component(100, "V")]
[ComponentGroup(ComponentGroup.H)]
[Description("A color model based on 'xyY'. Also known as 'Coloroid'.")]
[Hide]
[NotComplete]
[SuppressMessage("Style", "IDE1006:Naming Styles")]
public record class xyYC(double A, double T, double V)
    : Color3<xyYC, double, xyY>(A, T, V), IColor3<xyYC, double>, System.Numerics.IMinMaxValue<xyYC>
{
    public const int MaxHue = 76;

    public const int MinHue = 10;

    public static xyYC MaxValue => new(MaxHue, 100, 100);

    public static xyYC MinValue => new(MinHue, 0, 0);

    public xyYC() : this(default, default, default) { }

    public xyYC(double atv) : this(atv, atv, atv) { }

    public xyYC(IVector3<double> atv) : this(atv.X, atv.Y, atv.Z) { }

    public static readonly Dictionary<int, Vector> Colors = new()
    {
		//A         °     eλ        xλ       yλ
		{ 10,  new( 59.0, 1.724349, 0.44987, 0.53641) },
        { 11,  new( 55.3, 1.740844, 0.46248, 0.52444) },
        { 12,  new( 51.7, 1.754985, 0.47451, 0.51298) },
        { 13,  new( 48.2, 1.767087, 0.48601, 0.50325) },
        { 14,  new( 44.8, 1.775953, 0.49578, 0.49052) },
        { 15,  new( 41.5, 1.785073, 0.50790, 0.43035) },
        { 16,  new( 38.2, 1.791104, 0.51874, 0.46934) },
        { 20,  new( 34.9, 1.794831, 0.52980, 0.45783) },
        { 21,  new( 31.5, 1.798664, 0.54137, 0.44559) },
        { 22,  new( 28.0, 1.794819, 0.55367, 0.43253) },
        { 23,  new( 24.4, 1.789610, 0.56680, 0.41811) },
        { 24,  new( 20.6, 1.809483, 0.58128, 0.40176) },
        { 25,  new( 16.6, 1.760983, 0.59766, 0.38300) },
        { 26,  new( 12.3, 1.723443, 0.61653, 0.36061) },
        { 30,  new(  7.7, 1.652891, 0.63896, 0.33358) },
        { 31,  new(  2.8, 1.502607, 0.66619, 0.29930) },
        { 32,  new( -2.5, 1.072500, 0.70061, 0.26753) },
        { 33,  new( -8.4, 1.136637, 0.63925, 0.22631) },
        { 34,  new(-19.8, 1.232286, 0.53962, 0.19721) },
        { 35,  new(-31.6, 1.310120, 0.50340, 0.17495) },
        { 40,  new(-43.2, 1.376610, 0.46041, 0.15603) },
        { 41,  new(-54.6, 1.438692, 0.42386, 0.13846) },
        { 42,  new(-65.8, 1.501582, 0.38991, 0.12083) },
        { 43,  new(-76.8, 1.570447, 0.35586, 0.10328) },
        { 44,  new(-86.8, 1.645583, 0.32195, 0.08496) },
        { 45,  new(-95.8, 1.732083, 0.28657, 0.05155) },
        { 46,  new(-108.4,1.915753, 0.22202, 0.01771) },
        { 50,  new(-117.2,2.146310, 0.15664, 0.05227) },
        { 51,  new(-124.7,1.649939, 0.12736, 0.09020) },
        { 52,  new(-131.8,1.273415, 0.10813, 0.12506) },
        { 53,  new(-138.5,1.080809, 0.09414, 0.15741) },
        { 54,  new(-145.1,0.957076, 0.03249, 0.18958) },
        { 55,  new(-152.0,0.868976, 0.07206, 0.24109) },
        { 56,  new(-163.4,0.771731, 0.05787, 0.30378) },
        { 60,  new(-177.2,0.697108, 0.04353, 0.35696) },
        { 61,  new(171.6, 0.655803, 0.03291, 0.41971) },
        { 62,  new(152.4, 0.623958, 0.02240, 0.49954) },
        { 63,  new(148.4, 0.596037, 0.01196, 0.60321) },
        { 64,  new(136.8, 0.607413, 0.00425, 0.73542) },
        { 65,  new(125.4, 0.659923, 0.01099, 0.83391) },
        { 66,  new(114.2, 0.859517, 0.08050, 0.77474) },
        { 70,  new(103.2, 1.195683, 0.20259, 0.70460) },
        { 71,  new( 93.2, 1.407534, 0.28807, 0.65230) },
        { 72,  new( 84.2, 1.532829, 0.34422, 0.61930) },
        { 73,  new( 77.3, 1.603792, 0.37838, 0.59533) },
        { 74,  new( 71.6, 1.649448, 0.40290, 0.57716) },
        { 75,  new( 66.9, 1.681080, 0.42141, 0.56222) },
        { 76,  new( 62.8, 1.704979, 0.43647, 0.54895) }
    };

    /// <summary><see cref="Colors"/> sorted by angle.</summary>
    static readonly List<Vector> sColors;

#pragma warning disable CA1810 // Initialize reference type static fields inline
    static xyYC()
#pragma warning restore CA1810 // Initialize reference type static fields inline
    {
        var i = Colors.Count - 13;

        var a = Colors.Select(i => i.Value).Skip(i);
        var b = Colors.Select(i => i.Value).Take(i);

        sColors = a.Concat(b).ToList();
    }

    /// <summary><see cref="xyY"/> ⇒ <see cref="xyYC"/></summary>
    [NotComplete]
    public override void From(in xyY input, ColorProfile profile)
    {
        double x = X, y = input.Y, Y = Z;
        _ = (XYZ)(xyY)(XY)profile.Chromacity;
        var Xn = X * 100;
        var Yn = Y * 100;
        var Zn = Z * 100;

        var yM = Xn / (Xn + Yn + Zn);
        var xM = Yn / (Xn + Yn + Zn);
        var zM = (Xn + Yn + Zn) / 100;

        //Coloroid luminocity is the same as Labh lightness (the easier part)
        double V = 10 * Sqrt(Y);

        //Get the hue angle [-π, +π]
        double angle = Atan2(y - yM, x - xM) * 180 / PI;

        //Find the closest row
        Vector result;
        int j = sColors.Count - 1;
        int i;
        for (i = 0; i < sColors.Count; i++)
        {
            if (angle > sColors[i][1])
            {
                break;
            }
            j = i;
        }

        //Round instead of ceiling
        result = Abs(sColors[i + 1][1] - angle) > Abs(sColors[j][1] - angle) ? sColors[i + 1] : sColors[j];

        //Get hue id
        var A = result[0];

        //Calc saturation
        double xL = result[4], yL = result[2], zL = result[3];

        //xL should be scaled to [0, 100]
        double xyL = xL * yL * 100;

        double T = 100 * Y * (xM * zM - x * zM) / (100 * (x * yL - zL * yL) + xyL * (xM * zM - x * zM));
        //var T = 100 * Y * (1 - y * ew) / (100 * (y * el - yl * el) + Yl * (1 - y * ew));

        XYZ = new(A, T, V);
    }

    /// <summary><see cref="xyYC"/> ⇒ <see cref="xyY"/></summary>
    [NotComplete]
    public override void To(out xyY result, ColorProfile profile)
    {
        double A = X, T = XYZ.Y, V = Z;

        var xyzN = (XYZ)(xyY)(XY)profile.Chromacity;

        var Xn = X;
        var Yn = xyzN.Y;
        var Zn = Z;
        _ = Xn / (Xn + Yn + Zn);
        var xM = Yn / (Xn + Yn + Zn);
        var zM = (Xn + Yn + Zn) / 100;

        //Find the closest row
        Vector row = default;
        for (var i = 0; i < Colors.Count; i++)
        {
            if (A <= Colors[i][0])
            {
                row = Colors[i];
                break;
            }
        }

        double xL = row[4], yL = row[2], zL = row[3];

        var Y = V * V / 100;

        var xyL = xL * yL * 100;

        var x = (100 * Y * xM * zM + 100 * zL * yL * T - xyL * T * xM * zM) / (100 * T * yL - xyL * T * zM + 100 * Y * zM);
        var y = (100 * Y + 100 * T * xL * yL - xyL * T) / (Y * zM * 100 + T * 100 * yL - T * xyL * zM);

        //var x = (100*Y*ew*x0 + 100*T*el*xl - T*Yl*ew*x0) / (100*T*el - T*Yl*ew + 100*Y*ew);
        //var y = 100*Y / (100*T*el + 100*T*ew*Yl + 100*ew*Y);

        //var x = (ew*x0*(V*V - 100*T*row[6]) + 100*T*el*xl) / (ew*(V*V - 100*T*row[6]) + 100*T*el);
        //var y = V*V/(ew*(V*V + 100*T*row[6]) + 100*T*el);

        result = IColor.New<xyY>(x, y, Y);
    }
}