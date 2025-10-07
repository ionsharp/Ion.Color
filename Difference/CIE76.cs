using static System.Math;

namespace Ion.Colors;

/// <summary>CIE Delta-E 1976 color difference formula.</summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
[Name("CIE Delta-E 1976")]
public record class CIE76ColorDifference() : IColorDifference<Lab>, IColorDifference
{
    /// <param name="a">Reference color.</param>
    /// <param name="b">Sample color.</param>
    /// <returns>Delta-E (1976) color difference.</returns>
    public double ComputeDifference(in Lab a, in Lab b)
    {
        var distance = Sqrt
        (
            (a.X - b.X) * (a.X - b.X) +
            (a.Y - b.Y) * (a.Y - b.Y) +
            (a.Z - b.Z) * (a.Z - b.Z)
        );
        return distance;
    }

    /// <inheritdoc/>
    double IColorDifference.ComputeDifference(in IColor x, in IColor y) => ComputeDifference((Lab)x, (Lab)y);
}