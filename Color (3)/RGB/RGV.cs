using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <para><b>Orange (R), Spring green (G), Violet (V)</b></para>
/// <para>An additive model where the tertiary colors 'Orange' (red/yellow), 'Spring Green' (green/cyan), and 'Violet' (blue/magenta) are added together.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="RGV"/></para>
///
/// <i>Author</i>
/// <list type="bullet">
/// <item>Ion (2022)</item>
/// </list>
/// </summary>
[ColorOf<Lrgb>]
[Component(byte.MaxValue, "R", "Orange")]
[Component(byte.MaxValue, "G", "Spring green")]
[Component(byte.MaxValue, "V", "Violet")]
[Description("An additive model where the tertiary colors 'Orange' (red/yellow), 'Spring Green' (green/cyan), and 'Violet' (blue/magenta) are added together.")]
public record class RGV(byte R, byte G, byte V)
    : Color3<RGV, byte>(R, G, V), IColor3<RGV, byte>, System.Numerics.IMinMaxValue<RGV>
{
    public static RGV MaxValue => new(byte.MaxValue);

    public static RGV MinValue => new(byte.MinValue);

    public RGV() : this(default, default, default) { }

    public RGV(byte rgv) : this(rgv, rgv, rgv) { }

    public RGV(IVector3<byte> rgv) : this(rgv.X, rgv.Y, rgv.Z) { }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="RGV"/></summary>
    [NotTested]
    public override void From(in Lrgb input, ColorProfile profile)
    {
        //(100%) Red
        //> O = 75%, V = 25%

        //(100%) Green
        //> G = 75%, O = 25%

        //(100%) Blue
        //> V = 75%, G = 25%

        double r = input.X, g = input.Y, b = input.Z;
        XYZ = new Vector3<Double1>(0.75 * r + 0.25 * g, 0.75 * g + 0.25 * b, 0.75 * b + 0.25 * r).Denormalize<byte>();
    }

    /// <summary><see cref="RGV"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        //(100%) Orange
        //> R = 75%, G = 25%

        //(100%) Spring green
        //> G = 75%, B = 25%

        //(100%) Violet
        //> B = 75%, R = 25%

        var result = XYZ.Normalize(MinValue, MaxValue);
        double r = result.X, g = result.Y, v = result.Z;
        return new(0.75 * r + 0.25 * v, 0.75 * g + 0.25 * r, 0.75 * v + 0.25 * g);
    }
}