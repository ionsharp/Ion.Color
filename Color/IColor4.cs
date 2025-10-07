using Ion.Numeral;

namespace Ion.Colors;

/// <summary>An <see cref="IColor"/> with 4 components.</summary>
/// <remarks>Implements <see cref="IColor3"/>.</remarks>
public interface IColor4 : IColor3, IVector4
{
    new public const string Description = "A color with 4 components.";

    /// <summary>
    /// Get and set <see cref="Component4.W"/>.
    /// </summary>
    new public double W { get; set; }

    /// <summary>
    /// Get and set <see cref="Component4.X"/>, <see cref="Component4.Y"/>, <see cref="Component4.Z"/>, and <see cref="Component4.W"/>.
    /// </summary>
    public Vector4 XYZW { get; set; }
}