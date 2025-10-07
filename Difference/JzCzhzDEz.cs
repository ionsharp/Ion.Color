using Ion.Numeral;
using static System.Math;

namespace Ion.Colors;

/// <summary>Delta Ez color difference for <see cref="LCHabj"/>.</summary>
/// <remarks>
/// <para>https://github.com/tompazourek/Colourful</para>
/// <para>https://observablehq.com/@jrus/jzazbz</para>
/// </remarks>
[Name("Delta Ez")]
public sealed record class JzCzhzDEzColorDifference() : IColorDifference<LCHabj>, IColorDifference
{
    /// <inheritdoc/>
    public double ComputeDifference(in LCHabj x, in LCHabj y)
    {
        // conversion algorithm from: 

        var dJz = y.X - x.X;
        var dCz = y.Y - x.Y;
        var dhz = new Angle(y.Z).Convert(AngleType.Radian) - new Angle(x.Z).Convert(AngleType.Radian);
        var dHz2 = 2 * x.Y * y.Y * (1 - Cos(dhz));
        return Sqrt(dJz * dJz + dCz * dCz + dHz2);
    }

    /// <inheritdoc/>
    double IColorDifference.ComputeDifference(in IColor x, in IColor y) => ComputeDifference((LCHabj)x, (LCHabj)y);
}