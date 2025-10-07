using Ion.Numeral;
using static System.Math;

namespace Ion.Colors;

/// <summary>Euclidean distance between two colors.</summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
[Name("Euclidean")]
public sealed record class EuclideanColorDifference() : IColorDifference<IColor>, IColorDifference
{
    /// <inheritdoc/>
    public double ComputeDifference(in IColor x, in IColor y)
    {
        var distanceSquared = 0d;
        var vectorSize = Min(IVector3.Length, IVector3.Length);

        for (var i = 0; i < vectorSize; i++)
        {
            var xi = x.ToArray()[i];
            var yi = y.ToArray()[i];

            var xyiDiff = xi - yi;
            distanceSquared += xyiDiff * xyiDiff;
        }

        return Sqrt(distanceSquared);
    }

    /// <inheritdoc/>
    double IColorDifference.ComputeDifference(in IColor x, in IColor y) => ComputeDifference((IColor3)x, (IColor3)y);
}