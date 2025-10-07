using Ion.Numeral;

namespace Ion.Colors;

/// <summary>An <see cref="IColor"/> with 3 components.</summary>
/// <remarks>Implements <see cref="IColor2"/>.</remarks>
public interface IColor3 : IColor2, IVector3
{
    new public const string Description = "A color with 3 components.";

    /// <summary>
    /// Get and set <see cref="Component3.Z"/>.
    /// </summary>
    new public double Z { get; set; }

    /// <summary>
    /// Get and set <see cref="Component3.X"/>, <see cref="Component3.Y"/>, and <see cref="Component3.Z"/>.
    /// </summary>
    public Vector3 XYZ { get; set; }
}