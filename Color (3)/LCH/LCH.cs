using Ion.Numeral;
using System;
using static Ion.Numeral.Number;
using static System.Math;

namespace Ion.Colors;

public enum LCHio { X, Y, Z }

/// <summary>
/// <b>Luminance (L), Chroma (C), Hue (H)</b>
/// <para>A cylindrical color that is designed to accord with the human perception of color.</para>
/// </summary>
[Component(100, '%', "L", "Luminance")]
[Component(100, '%', "C", "Chroma")]
[Component(360, '°', "H", "Hue")]
public record class LCH<TSelf, TOther>(double L, double C, double H)
    : Color3<TSelf, double>(L, C, H)
    where TSelf : IColor3<TSelf, double>, System.Numerics.IMinMaxValue<TSelf>
    where TOther : IColor3, new()
{
    /// <summary>One of three experimental, derived forms of <see cref="LCH{TSelf, TOther}"/>.</summary>
    /// <param name="i">The color to convert.</param>
    /// <remarks>See <see cref="LCHio"/>.</remarks>
    /// <returns>A color in <see cref="LCHio.X"/> space.</returns>
    private static Vector3 FromLChiox(Vector3 i)
    {
        double u = new Vector2(i.Z / 360, i.Y / 100).Distance();
        double v = new Vector2(i.Z / 360, i.X / 100).Distance();
        return new Vector3(i.X, i.Y, Math.Clamp(i.Z * u / v, 0, 359));
    }

    /// <summary>One of three experimental, derived forms of <see cref="LCH{TSelf, TOther}"/>.</summary>
    /// <param name="i">The color to convert.</param>
    /// <remarks>See <see cref="LCHio"/>.</remarks>
    /// <returns>A color in <see cref="LCHio.Y"/> space.</returns>
    private static Vector3 FromLChioy(Vector3 i)
    {
        double w = new Vector2(i.Y / 100, i.X / 100).Distance();
        return new Vector3(i.X, i.Y, i.Z * w);
    }

    /// <summary>One of three experimental, derived forms of <see cref="LCH{TSelf, TOther}"/>.</summary>
    /// <param name="i">The color to convert.</param>
    /// <remarks>See <see cref="LCHio"/>.</remarks>
    /// <returns>A color in <see cref="LCHio.Z"/> space.</returns>
    private static Vector3 FromLChioz(Vector3 i)
    {
        double l = i.X / 100;
        double c = i.Y / 100;
        double h = i.Z / 360;

        double u = new Vector2(h, c).Distance();
        double v = new Vector2(h, l).Distance();

        double x = new Vector2(c, c).Distance();
        double y = new Vector2(c, l).Distance();
        return new Vector3(i.X, Math.Clamp(i.Y * x / y, 0, 100), Math.Clamp(i.Z * u / v, 0, 359));
    }

    ///

    public virtual Vector3 FromLCh(Vector3 input)
    {
        double c = input.Y, h = input.Z;
        h = new Angle(h).Convert(AngleType.Radian);

        var a = c * Cos(h);
        var b = c * Sin(h);
        return new(input.X, a, b);
    }

    public virtual Vector3 ToLCh(Vector3 input)
    {
        double a = input.Y, b = input.Z;

        var hr = Atan2(b, a);
        var h = new Angle(hr, AngleType.Radian).Convert(AngleType.Degree);

        var c = Sqrt(a * a + b * b);
        return new(input.X, c, h);
    }

    public override void From(in Lrgb input, ColorProfile profile)
    {
        var result = new TOther();
        result.From(input, profile);

        XYZ = ToLCh(result.XYZ);
    }

    public override Lrgb To(ColorProfile profile)
    {
        TOther result = new();
        result.XYZ = FromLCh(XYZ);

        return result.To(profile);
    }
}