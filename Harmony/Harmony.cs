using System;

namespace Ion.Colors;

/// <remarks>https://www.colorsexplained.com/color-harmony/</remarks>
public enum Harmony
{
    /// <summary>Colors with three hues, all positioned next to each other on the color wheel. </summary>
    Analogous,
    /// <summary>Warm colors range from magenta to yellow and cool colors range from violet to green.</summary>
    [Name("Cool/warm")]
    CoolWarm,
    /// <summary>Colors positioned on opposite ends of the color wheel.</summary>
    Diad,
    /// <summary>Two sets of complementary colors..</summary>
    DoubleComplementary,
    /// <summary>A single base hue with different shades, tones, and tints of that color family.</summary>
    Monochromatic,
    /// <summary>Complementary colors, but with three hues (usually, one key color and two colors adjacent to that key color’s complement).</summary>
    [Name("Split-Complementary")]
    SplitComplementary,
    /// <summary>Four colors spaced evenly around the color wheel.</summary>
    Square,
    /// <summary>Three colors evenly spaced on the color wheel.</summary>
    Triad,
    /// <summary>A key color and three more colors, all equidistant from the key color on the color wheel (also called <b>double-complementary</b>).</summary>
    Tetrad,
}