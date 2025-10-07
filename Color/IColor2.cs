using Ion.Numeral;

namespace Ion.Colors;

/// <summary>An <see cref="IColor"/> with 2 components.</summary>
/// <remarks>Implements <see cref="IColor"/>.</remarks>
public interface IColor2 : IColor, IVector2
{
    new public const string Description = "A color with 2 components.";

    /// <summary>
    /// Get and set <see cref="Component2.X"/>.
    /// </summary>
    new public double X { get; set; }

    /// <summary>
    /// Get and set <see cref="Component2.Y"/>.
    /// </summary>
    new public double Y { get; set; }

    /// <summary>
    /// Get and set <see cref="Component2.X"/> and <see cref="Component2.Y"/>.
    /// </summary>
    public Vector2 XY { get; set; }
}