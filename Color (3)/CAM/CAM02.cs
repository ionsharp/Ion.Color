using Ion.Numeral;
using System;
using static System.Math;

namespace Ion.Colors;

/// <summary>Constants used by <see cref="CAM02{TSelf}"/></summary>
public static class CAM02
{
    public static Matrix3x3<double> Aab_RGB => new
    (
        0.32787, 0.32145, 0.20527,
        0.32787, -0.63507, -0.18603,
        0.32787, -0.15681, -4.49038
    );

    public static Matrix3x3<double> CAT02_HPE => new
    (
         0.7409792, 0.2180250, 0.0410058,
         0.2853532, 0.6242014, 0.0904454,
        -0.0096280, -0.0056980, 1.0153260
    );

    public static Matrix3x3<double> CAT02_XYZ => new
    (
         1.096124, -0.278869, 0.182745,
         0.454369, 0.473533, 0.072098,
        -0.009628, -0.005698, 1.015326
    );

    public static Matrix3x3<double> HPE_XYZ => new
    (
        1.910197, -1.112124, 0.201908,
        0.370950, 0.629054, -0.000008,
        0.000000, 0.000000, 1.000000
    );

    public static double NonlinearAdaptation(double c, double FL)
    {
        double p = Pow((FL * c) / 100.0, 0.42);
        return ((400.0 * p) / (27.13 + p)) + 0.1;
    }

    public static double NonlinearAdaptationInverse(double c, double FL)
        => (100.0 / FL) * Pow((27.13 * Abs(c - 0.1)) / (400.0 - Abs(c - 0.1)), 1.0 / 0.42);
}

/// <summary>
/// <b>CAM02</b>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="CAM02"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>CIECAM02</item>
/// </list>
/// 
/// <i>Authors</i>
/// <list type="bullet">
/// <item><see cref="CIE"/> (2002)</item>
/// <item><see href="vektor@dumbterm.net">Billy Biggs</see> (2003)</item>
/// </list>
/// 
/// <i>Versions</i>
/// <list type="bullet">
/// <item>( - ) Ported from C++.</item>
/// <item>(0.4) Various revisions by Nathan Moroney of HP Labs.</item>
/// <item>(0.3) Further cleanups and a function to return all of J,C,h,Q,M,s.</item>
/// <item>(0.2) Cleanup, added missing functions.</item>
/// <item>(0.1) Initial release</item>
/// </list>
/// </summary>
/// <remarks>http://scanline.ca/ciecam02/ciecam02.c</remarks>
[ColorOf<XYZ>]
[NotAccurate]
[Refactor]
public abstract partial record class CAM02<TSelf>(double X, double Y, double Z)
    : Color3<TSelf, double, XYZ>(X, Y, Z)
    where TSelf : System.Numerics.IMinMaxValue<TSelf>, IColor3<TSelf, double>
{
#pragma warning disable IDE1006
    public double aC { get; private set; }
    public double bC { get; private set; }

    public double aS { get; private set; }
    public double bS { get; private set; }

    public double aM { get; private set; }
    public double bM { get; private set; }

    ///

    public double H { get; private set; }

    ///

    public abstract double J { get; set; }

    public abstract double Q { get; set; }

    public abstract double C { get; set; }

    public abstract double M { get; set; }

    public abstract double s { get; set; }

    public double h { get => Z; set => Z = value; }
#pragma warning restore

    /// <summary><see cref="XYZ"/> ⇒ <see cref="CAM02"/></summary>
    [NotComplete]
    protected static T From<T>(XYZ input, ColorProfile profile) where T : CAM02<TSelf>, new()
    {
        input.XYZ *= new Vector3(100);

        var vc = profile.Conditions;
        double rc, gc, bc;
        double rpa, gpa, bpa;
        double a, ca, cb;
        double et, t, temp;

        var rgb = ChromacityAdaptationTransform.CAT02 * new Vector3(input.X, input.Y, input.Z);
        var r = rgb.X;
        var g = rgb.Y;
        var b = rgb.Z;

        var rgbw = ChromacityAdaptationTransform.CAT02 * new Vector3(profile.White.X * 100, profile.White.Y * 100, profile.White.Z * 100);
        var rw = rgbw.X;
        var gw = rgbw.Y;
        var bw = rgbw.Z;

        rc = r * (((profile.White.Y * 100 * vc.D) / rw) + (1.0 - vc.D));
        gc = g * (((profile.White.Y * 100 * vc.D) / gw) + (1.0 - vc.D));
        bc = b * (((profile.White.Y * 100 * vc.D) / bw) + (1.0 - vc.D));

        var rgbp = CAM02.CAT02_HPE * new Vector3(rc, gc, bc);
        var rp = rgbp.X;
        var gp = rgbp.Y;
        var bp = rgbp.Z;

        rpa = CAM02.NonlinearAdaptation(rp, vc.FL);
        gpa = CAM02.NonlinearAdaptation(gp, vc.FL);
        bpa = CAM02.NonlinearAdaptation(bp, vc.FL);

        ca = rpa - ((12.0 * gpa) / 11.0) + (bpa / 11.0);
        cb = (1.0 / 9.0) * (rpa + gpa - (2.0 * bpa));

        T i = new()
        {
            h = (180.0 / PI) * Atan2(cb, ca)
        };
        if (i.h < 0.0) i.h += 360.0;

        if (i.h < 20.14)
        {
            temp = ((i.h + 122.47) / 1.2) + ((20.14 - i.h) / 0.8);
            i.H = 300 + (100 * ((i.h + 122.47) / 1.2)) / temp;
        }
        else if (i.h < 90.0)
        {
            temp = ((i.h - 20.14) / 0.8) + ((90.00 - i.h) / 0.7);
            i.H = (100 * ((i.h - 20.14) / 0.8)) / temp;
        }
        else if (i.h < 164.25)
        {
            temp = ((i.h - 90.00) / 0.7) + ((164.25 - i.h) / 1.0);
            i.H = 100 + ((100 * ((i.h - 90.00) / 0.7)) / temp);
        }
        else if (i.h < 237.53)
        {
            temp = ((i.h - 164.25) / 1.0) + ((237.53 - i.h) / 1.2);
            i.H = 200 + ((100 * ((i.h - 164.25) / 1.0)) / temp);
        }
        else
        {
            temp = ((i.h - 237.53) / 1.2) + ((360 - i.h + 20.14) / 0.8);
            i.H = 300 + ((100 * ((i.h - 237.53) / 1.2)) / temp);
        }

        a = ((2.0 * rpa) + gpa + ((1.0 / 20.0) * bpa) - 0.305) * vc.Nbb;

        i.J = 100.0 * Pow(a / vc.Aw, vc.c * vc.z);

        et = (1.0 / 4.0) * (Cos(((i.h * PI) / 180.0) + 2.0) + 3.8);
        t = ((50000.0 / 13.0) * vc.Nc * vc.Ncb * et * Sqrt((ca * ca) + (cb * cb))) / (rpa + gpa + (21.0 / 20.0) * bpa);

        i.C = Pow(t, 0.9) * Sqrt(i.J / 100.0) * Pow(1.64 - Pow(0.29, vc.n), 0.73);

        i.Q = (4.0 / vc.c) * Sqrt(i.J / 100.0) * (vc.Aw + 4.0) * Pow(vc.FL, 0.25);

        i.M = i.C * Pow(vc.FL, 0.25);

        i.s = 100.0 * Sqrt(i.M / i.Q);

        i.aC = i.C * Cos(i.h * PI / 180.0);
        i.bC = i.C * Sin(i.h * PI / 180.0);

        i.aM = i.M * Cos(i.h * PI / 180.0);
        i.bM = i.M * Sin(i.h * PI / 180.0);

        i.aS = i.s * Cos(i.h * PI / 180.0);
        i.bS = i.s * Sin(i.h * PI / 180.0);
        return (i);
    }

    /// <summary><see cref="CAM02"/> ⇒ <see cref="XYZ"/></summary>
    [NotComplete]
    public override void To(out XYZ result, ColorProfile profile)
    {
        var input = this;

        var vc = profile.Conditions;

        double r, g, b;
        double rp, gp, bp;
        double a, ca, cb;
        double et, t;
        double p1, p2, p3, p4, p5, hr;
        var rgbw = ChromacityAdaptationTransform.CAT02 * new Vector3(profile.White.X * 100, profile.White.Y * 100, profile.White.Z * 100);
        var rw = rgbw.X;
        var gw = rgbw.Y;
        var bw = rgbw.Z;

        t = Pow(input.C / (Sqrt(input.J / 100.0) * Pow(1.64 - Pow(0.29, vc.n), 0.73)), (1.0 / 0.9));
        et = (1.0 / 4.0) * (Cos(((input.h * PI) / 180.0) + 2.0) + 3.8);

        a = Pow(input.J / 100.0, 1.0 / (vc.c * vc.z)) * vc.Aw;

        p1 = ((50000.0 / 13.0) * vc.Nc * vc.Ncb) * et / t;
        p2 = (a / vc.Nbb) + 0.305;
        p3 = 21.0 / 20.0;

        hr = (input.h * PI) / 180.0;

        if (Abs(Sin(hr)) >= Abs(Cos(hr)))
        {
            p4 = p1 / Sin(hr);
            cb = (p2 * (2.0 + p3) * (460.0 / 1403.0)) /
                (p4 + (2.0 + p3) * (220.0 / 1403.0) *
                (Cos(hr) / Sin(hr)) - (27.0 / 1403.0) +
                p3 * (6300.0 / 1403.0));
            ca = cb * (Cos(hr) / Sin(hr));
        }
        else
        {
            p5 = p1 / Cos(hr);
            ca = (p2 * (2.0 + p3) * (460.0 / 1403.0)) /
                    (p5 + (2.0 + p3) * (220.0 / 1403.0) -
                    ((27.0 / 1403.0) - p3 * (6300.0 / 1403.0)) *
                    (Sin(hr) / Cos(hr)));
            cb = ca * (Sin(hr) / Cos(hr));
        }

        var rgbpa = CAM02.Aab_RGB * new Vector3((a / vc.Nbb) + 0.305, ca, cb);
        var rpa = rgbpa.X;
        var gpa = rgbpa.Y;
        var bpa = rgbpa.Z;

        rp = CAM02.NonlinearAdaptationInverse(rpa, vc.FL);
        gp = CAM02.NonlinearAdaptationInverse(gpa, vc.FL);
        bp = CAM02.NonlinearAdaptationInverse(bpa, vc.FL);

        var xyzt = CAM02.HPE_XYZ * new Vector3(rp, gp, bp);
        var tx = xyzt.X;
        var ty = xyzt.Y;
        var tz = xyzt.Z;
        var rgbc = ChromacityAdaptationTransform.CAT02 * new Vector3(tx, ty, tz);
        var rc = rgbc.X;
        var gc = rgbc.Y;
        var bc = rgbc.Z;

        r = rc / (((profile.White.Y * 100 * vc.D) / rw) + (1.0 - vc.D));
        g = gc / (((profile.White.Y * 100 * vc.D) / gw) + (1.0 - vc.D));
        b = bc / (((profile.White.Y * 100 * vc.D) / bw) + (1.0 - vc.D));

        var xyz = CAM02.CAT02_XYZ * new Vector3(r, g, b);
        result = IColor.New<XYZ>(xyz);
    }
}