using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <para><b>Rose (R), Chartreuse (C), Azure (A)</b></para>
/// <para>An additive model where the tertiary colors 'Rose' (red/magenta), 'Chartreuse' (green/yellow), and 'Azure' (blue/cyan) are added together.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="RCA"/></para>
///
/// <i>Author</i>
/// <list type="bullet">
/// <item>Ion (2022)</item>
/// </list>
/// </summary>
[ColorOf<Lrgb>]
[Component(byte.MaxValue, "R", "Rose")]
[Component(byte.MaxValue, "C", "Chartreuse")]
[Component(byte.MaxValue, "A", "Azure")]
[Description("An additive model where the tertiary colors 'Rose' (red/magenta), 'Chartreuse' (green/yellow), and 'Azure' (blue/cyan) are added together.")]
public record class RCA(byte R, byte C, byte A)
    : Color3<RCA, byte>(R, C, A), IColor3<RCA, byte>, System.Numerics.IMinMaxValue<RCA>
{
    public static RCA MaxValue => new(byte.MaxValue);

    public static RCA MinValue => new(byte.MinValue);

    public RCA() : this(default, default, default) { }

    public RCA(byte rca) : this(rca, rca, rca) { }

    public RCA(IVector3<byte> rca) : this(rca.X, rca.Y, rca.Z) { }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="RCA"/></summary>
    [NotTested]
    public override void From(in Lrgb input, ColorProfile profile)
    {
        //(100%) Red
        //> R = 75%, C = 25%

        //(100%) Green
        //> C = 75%, A = 25%

        //(100%) Blue
        //> A = 75%, R = 25%

        double r = input.X, g = input.Y, b = input.Z;
        XYZ = new Vector3<Double1>(0.75 * r + 0.25 * b, 0.75 * g + 0.25 * r, 0.75 * b + 0.25 * g).Denormalize<byte>();
    }

    /// <summary><see cref="RCA"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        //(100%) Rose
        //> R = 75%, B = 25%

        //(100%) Chartreuse
        //> G = 75%, R = 25%

        //(100%) Azure
        //> B = 75%, G = 25%

        var result = XYZ.Normalize(MinValue, MaxValue);
        double r = result.X, c = result.Y, a = result.Z;
        return new(0.75 * r + 0.25 * c, 0.75 * c + 0.25 * a, 0.75 * a + 0.25 * r);
    }
}