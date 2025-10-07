using Ion.Numeral;
using System;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <para><b>U*, V*, W*</b></para>
/// <para>A color model based on 'UCS' that was invented to calculate color differences without having to hold the luminance constant.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="UVW"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>CIEUVW</item>
/// <item>U*V*W*</item>
/// </list>
/// 
/// <i>Author</i>
/// <list type="bullet">
/// <item><see cref="CIE"/> (1964)</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/uvw.js</remarks>
[ColorOf<XYZ>]
[Component(-134, 224, ' ', "U*")]
[Component(-140, 122, ' ', "V*")]
[Component(100, '%', "W*")]
[Description("A color model based on 'UCS' that was invented to calculate color differences without having to hold the luminance constant.")]
public record class UVW(double U, double V, double W)
    : Color3<UVW, double, XYZ>(U, V, W), IColor3<UVW, double>, System.Numerics.IMinMaxValue<UVW>
{
    public static UVW MaxValue => new(+224, +122, 100);

    public static UVW MinValue => new(-134, -140, 0);

    public UVW() : this(default, default, default) { }

    public UVW(double uvw) : this(uvw, uvw, uvw) { }

    public UVW(IVector3<double> uvw) : this(uvw.X, uvw.Y, uvw.Z) { }

    /// <summary><see cref="XYZ"/> ⇒ <see cref="UVW"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
    {
        double x = input.X, y = input.Y, z = input.Z, xn, yn, zn, un, vn;

        xn = profile.White.X; yn = profile.White.Y; zn = profile.White.Z;
        un = (4 * xn) / (xn + (15 * yn) + (3 * zn));
        vn = (6 * yn) / (xn + (15 * yn) + (3 * zn));

        var uv = x + 15 * y + 3 * z;
        var _u = uv == 0 ? 0 : 4 * x / uv;
        var _v = uv == 0 ? 0 : 6 * y / uv;

        var w = 25 * Pow(y, 1 / 3) - 17;
        var u = 13 * w * (_u - un);
        var v = 13 * w * (_v - vn);
        XYZ = new(u, v, w);
    }

    /// <summary><see cref="UVW"/> ⇒ <see cref="XYZ"/></summary>
    public override void To(out XYZ result, ColorProfile profile)
    {
        double _u, _v, w, u, v, x, y, z, xn, yn, zn, un, vn;
        u = X; v = Y; w = Z;

        if (w == 0)
        {
            result = IColor.New<XYZ>(0);
            return;
        }

        xn = profile.White.X; yn = profile.White.Y; zn = profile.White.Z;
        un = (4 * xn) / (xn + (15 * yn) + (3 * zn));
        vn = (6 * yn) / (xn + (15 * yn) + (3 * zn));

        y = Convert.ToSingle(Pow((w + 17f) / 25f, 3f));

        _u = 13 * w == 0 ? 0 : u / (13 * w) + un;
        _v = 13 * w == 0 ? 0 : v / (13 * w) + vn;

        x = (6 / 4) * y * _u / _v;
        z = Convert.ToSingle(y * (2 / _v - 0.5 * _u / _v - 5));

        result = IColor.New<XYZ>(new Vector3(x, y, z) / 100);
    }
}