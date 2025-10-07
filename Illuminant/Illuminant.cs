using Ion.Numeral;

namespace Ion.Colors;

/// <summary>
/// Represents a theoretical source of visible light with a published profile.
/// </summary>
/// <remarks>
/// <para>https://github.com/tompazourek/Colourful</para>
/// <para>https://en.wikipedia.org/wiki/Standard_illuminant</para>
/// </remarks>
public static class Illuminant
{
    /// <summary>An arbitrary illuminant when none is specified (<see cref="Illuminant2.D65"/>).</summary>
    public static XYZ Default => IColor.New<XY>(Illuminant2.D65).To<XYZ>(default);

    /// <summary> Equal energy (<see cref="Illuminant">5454</see> K).</summary>
    [Description("Equal energy")]
    public static Vector2 E => new(0.33333, 0.33333);
}