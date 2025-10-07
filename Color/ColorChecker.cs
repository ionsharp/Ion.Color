using ColorList = System.Collections.Generic.IReadOnlyList<Ion.Colors.RGB>;

namespace Ion.Colors;

/// <summary>Colors of the Macbeth ColorChecker. Assume that the RGB colors are in sRGB working space.</summary>
/// <remarks>
/// <para>https://github.com/tompazourek/Colourful</para>
/// <para>http://xritephoto.com/documents/literature/en/ColorData-1p_EN.pdf</para>
/// </remarks>
public static class ColorChecker
{
    /// <summary>
    /// Dark skin (color #1).
    /// </summary>
    public static readonly RGB DarkSkin = new(115, 82, 68);

    /// <summary>
    /// Light skin (color #2).
    /// </summary>
    public static readonly RGB LightSkin = new(194, 150, 130);

    /// <summary>
    /// Blue sky (color #3).
    /// </summary>
    public static readonly RGB BlueSky = new(98, 122, 157);

    /// <summary>
    /// Foliage (color #4).
    /// </summary>
    public static readonly RGB Foliage = new(87, 108, 67);

    /// <summary>
    /// Blue flower (color #5).
    /// </summary>
    public static readonly RGB BlueFlower = new(133, 128, 177);

    /// <summary>
    /// Bluish green (color #6).
    /// </summary>
    public static readonly RGB BluishGreen = new(103, 189, 170);

    /// <summary>
    /// Orange (color #7).
    /// </summary>
    public static readonly RGB Orange = new(214, 126, 44);

    /// <summary>
    /// Purplish blue (color #8).
    /// </summary>
    public static readonly RGB PurplishBlue = new(80, 91, 166);

    /// <summary>
    /// Moderate red (color #9).
    /// </summary>
    public static readonly RGB ModerateRed = new(193, 90, 99);

    /// <summary>
    /// Purple (color #10).
    /// </summary>
    public static readonly RGB Purple = new(94, 60, 108);

    /// <summary>
    /// Yellow green (color #11).
    /// </summary>
    public static readonly RGB YellowGreen = new(157, 188, 64);

    /// <summary>
    /// Orange Yellow (color #12).
    /// </summary>
    public static readonly RGB OrangeYellow = new(224, 163, 46);

    /// <summary>
    /// Blue (color #13).
    /// </summary>
    public static readonly RGB Blue = new(56, 61, 150);

    /// <summary>
    /// Green (color #14).
    /// </summary>
    public static readonly RGB Green = new(70, 148, 73);

    /// <summary>
    /// Red (color #15).
    /// </summary>
    public static readonly RGB Red = new(175, 54, 60);

    /// <summary>
    /// Yellow (color #16).
    /// </summary>
    public static readonly RGB Yellow = new(231, 199, 31);

    /// <summary>
    /// Magenta (color #17).
    /// </summary>
    public static readonly RGB Magenta = new(187, 86, 149);

    /// <summary>
    /// Cyan (color #18).
    /// </summary>
    public static readonly RGB Cyan = new(8, 133, 161);

    /// <summary>
    /// White (color #19).
    /// </summary>
    public static readonly RGB White = new(243, 243, 242);

    /// <summary>
    /// Neutral 8 (color #20).
    /// </summary>
    public static readonly RGB Neutral8 = new(200, 200, 200);

    /// <summary>
    /// Neutral 6.5 (color #21).
    /// </summary>
    public static readonly RGB Neutral6p5 = new(160, 160, 160);

    /// <summary>
    /// Neutral 5 (color #22).
    /// </summary>
    public static readonly RGB Neutral5 = new(122, 122, 121);

    /// <summary>
    /// Neutral 3.5 (color #23).
    /// </summary>
    public static readonly RGB Neutral3p5 = new(85, 85, 85);

    /// <summary>
    /// Black (color #24).
    /// </summary>
    public static readonly RGB Black = new(52, 52, 52);

    /// <summary>
    /// Array of 24 colors of the Macbeth ColorChecker.
    /// </summary>
    public static readonly ColorList Colors = [DarkSkin, LightSkin, BlueSky, Foliage, BlueFlower, BluishGreen, Orange, PurplishBlue, ModerateRed, Purple, YellowGreen, OrangeYellow, Blue, Green, Red, Yellow, Magenta, Cyan, White, Neutral8, Neutral6p5, Neutral5, Neutral3p5, Black];
}