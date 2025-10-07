using System.ComponentModel;

namespace Ion.Colors;

#pragma warning disable IDE1006
/// <summary>
/// <para>http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html</para>
/// <para>https://github.com/tompazourek/Colourful</para>
/// </summary>
public static class ColorProfiles
{
    enum Category { Adobe, Apple, CIE, DCI, Don, ECI, Fraser, Holmes, Ion, ITU, Kodak, Lindbloom, Microsoft, NTSC, PAL, Radius }

    /// <see cref="Category.Adobe"/>
    #region

    /// <summary>
    /// <b>Adobe RGB (1998)</b>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// </summary>
    [Category(nameof(Category.Adobe)), Name("Adobe RGB (1998)")]
    [Description("Designed to encompass most of the colors achievable on CMYK color printers, but by using RGB primary colors on a device such as a computer display.")]
    public static ColorProfile AdobeRGB1998
        => new(Illuminant2.D65, new(0.640, 0.330), new(0.210, 0.710), new(0.150, 0.060), new GammaCompression(2.2));

    /// <summary>
    /// <b>Wide Gamut RGB</b>
    /// <para><see cref="Illuminant2.D50"/></para>
    /// </summary>
    [Category(nameof(Category.Adobe)), Name("Wide Gamut RGB")]
    [Description("Offers a large gamut by using pure spectral primary colors.")]
    public static ColorProfile WideGamutRGB
        => new(Illuminant2.D50, new(0.735, 0.265), new(0.115, 0.826), new(0.157, 0.018), new GammaCompression(2.2));

    #endregion

    /// <see cref="Category.Apple"/>
    #region

    /// <summary>
    /// <b>Apple RGB</b>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// </summary>
    [Category(nameof(Category.Apple)), Name("Apple RGB")]
    [Description("")]
    public static ColorProfile AppleRGB
        => new(Illuminant2.D65, new(0.625, 0.340), new(0.280, 0.595), new(0.155, 0.070), new GammaCompression(1.8));

    /// <summary>
    /// <b>Cinema Gamut</b>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// </summary>
    [Category(nameof(Category.Apple)), Name("Cinema Gamut")]
    [Description("")]
    public static ColorProfile CinemaGamut
        => new(Illuminant2.D65, new(0.740, 0.270), new(0.170, 1.140), new(0.080, -0.100), new GammaCompression(2.6));

    /// <summary>
    /// <b>P3-D65 (Display)</b>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// </summary>
    [Category(nameof(Category.Apple)), Name("P3-D65 (Display)")]
    [Description("")]
    public static ColorProfile Display_P3
        => new(Illuminant2.D65, new(0.680, 0.320), new(0.265, 0.690), new(0.150, 0.060), new Compression(12 / 5, 0.055, 0.0031308, 12.92, 0.04045));

    #endregion

    /// <see cref="Category.CIE"/>
    #region

    /// <summary>
    /// <b>CIE-RGB</b>
    /// <para><see cref="Illuminant.E"/></para>
    /// </summary>
    [Category(nameof(Category.CIE)), Name("CIE-RGB")]
    [Description("")]
    public static ColorProfile CIE_RGB
        => new(Illuminant.E, new(0.735, 0.265), new(0.274, 0.717), new(0.167, 0.009), new GammaCompression(2.2));

    /// <summary>
    /// <b>CIE-XYZ</b>
    /// <para><see cref="Illuminant.E"/></para>
    /// </summary>
    [Category(nameof(Category.CIE)), Name("CIE-XYZ"), NotComplete]
    [Description("")]
    public static ColorProfile CIE_XYZ
        => new(Illuminant.E, new(1.000, 0.000), new(0.000, 1.000), new(0.000, 0.000), new GammaCompression(2.2));

    #endregion

    /// <see cref="Category.DCI"/>
    #region

    /// <summary>
    /// <b>P3-D60 (ACES Cinema)</b>
    /// <para><see cref="Illuminant2.D60"/></para>
    /// </summary>
    [Category(nameof(Category.DCI)), Name("P3-D60 (ACES Cinema)")]
    [Description("")]
    public static ColorProfile Cinema_P3
        => new(Illuminant2.D60, new(0.680, 0.320), new(0.265, 0.690), new(0.150, 0.060), new GammaCompression(2.6));

    /// <summary>
    /// <b>DCI-P3+</b>
    /// <para><see cref="Illuminant2.D63"/></para>
    /// </summary>
    [Category(nameof(Category.DCI)), Name("DCI-P3+")]
    [Description("")]
    public static ColorProfile DCI_P3Plus
        => new(Illuminant2.D63, new(0.740, 0.270), new(0.220, 0.780), new(0.090, -0.090), new GammaCompression(2.6));

    /// <summary>
    /// <b>P3-DCI (Theater)</b>
    /// <para><see cref="Illuminant2.D63"/></para>
    /// </summary>
    [Category(nameof(Category.DCI)), Name("P3-DCI (Theater)")]
    [Description("")]
    public static ColorProfile Theater_P3
        => new(Illuminant2.D63, new(0.680, 0.320), new(0.265, 0.690), new(0.150, 0.060), new GammaCompression(2.6));

    #endregion

    /// <see cref="Category.Don"/>
    #region

    /// <summary>
    /// <b>Best RGB</b>
    /// <para><see cref="Illuminant2.D50"/></para>
    /// </summary>
    [Category(nameof(Category.Don)), Name("Best RGB")]
    [Description("")]
    public static ColorProfile BestRGB
        => new(Illuminant2.D50, new(0.7347, 0.2653), new(0.2150, 0.7750), new(0.1300, 0.0350), new GammaCompression(2.2));

    /// <summary>
    /// <b>Don RGB 4</b>
    /// <para><see cref="Illuminant2.D50"/></para>
    /// </summary>
    [Category(nameof(Category.Don)), Name("Don RGB 4")]
    [Description("")]
    public static ColorProfile DonRGB4
        => new(Illuminant2.D50, new(0.696, 0.300), new(0.215, 0.765), new(0.130, 0.035), new GammaCompression(2.2));

    #endregion

    /// <see cref="Category.ECI"/>
    #region

    /// <summary>
    /// <b>eciRGB</b>
    /// <para><see cref="Illuminant2.D50"/></para>
    /// </summary>
    [Category(nameof(Category.ECI)), Name("eciRGB")]
    [Description("")]
    public static ColorProfile eciRGB
        => new(Illuminant2.D50, new(0.670, 0.330), new(0.210, 0.710), new(0.140, 0.080), new Compression(3, 1.16, 0.008856, 9.033, 0.08));

    #endregion

    /// <see cref="Category.Fraser"/>
    #region

    /// <summary>
    /// <b>Bruce RGB</b>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// <para>A conservative-gamut space for dealing with 8-bit imagery that needs heavy editing.</para>
    /// </summary>
    [Category(nameof(Category.Fraser)), Name("Bruce RGB")]
    [Description("A conservative-gamut space for dealing with 8-bit imagery that needs heavy editing.")]
    public static ColorProfile BruceRGB
        => new(Illuminant2.D65, new(0.640, 0.330), new(0.280, 0.650), new(0.150, 0.060), new GammaCompression(2.2));

    #endregion

    /// <see cref="Category.Ion"/>
    #region

    /// <summary>
    /// <b>sRGB-HLG</b>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// <para><see cref="sRGB"/> with hybrid log-gamma (HLG) compression.</para>
    /// </summary>
    [Category(nameof(Category.Ion)), Name("sRGB-HLG")]
    [Description("sRGB with hybrid log-gamma (HLG) compression.")]
    public static ColorProfile sRGB_HLG
        => new(ColorProfile.sRGBWhite, ColorProfile.sRGBPrimary, new GammaLogCompression());

    /// <summary>
    /// <b>sRGB-PQ</b>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// <para><see cref="sRGB"/> with perceptual quantization (PQ) compression.</para>
    /// </summary>
    [Category(nameof(Category.Ion)), Name("sRGB-PQ")]
    [Description("sRGB with perceptual quantization (PQ) compression.")]
    public static ColorProfile sRGB_PQ
        => new(ColorProfile.sRGBWhite, ColorProfile.sRGBPrimary, new PQCompression());

    #endregion

    /// <see cref="Category.ITU"/>
    #region

    /// <summary>
    /// <b>HDTV</b>
    /// <para><i>ITU-R BT.709</i> (Rec. 709)</para>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// </summary>
    [Category(nameof(Category.ITU)), Name("HDTV")]
    [Description("")]
    public static ColorProfile HDTV
        => new(Illuminant2.D65, new(0.640, 0.330), new(0.300, 0.600), new(0.150, 0.060), new Compression(20 / 9, 0.099, 0.004, 4.5, 0.018));

    /// <summary>
    /// <b>MAC</b>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// </summary>
    [Category(nameof(Category.ITU)), Name("MAC")]
    [Description("")]
    public static ColorProfile MAC
        => new(Illuminant2.D65, new(0.670, 0.330), new(0.210, 0.710), new(0.140, 0.080), new GammaCompression(2.8));

    /// <summary>
    /// <b>UHDTV</b> (2020)
    /// <para><i>ITU-R BT.2020</i> (Rec. 2020)</para>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// </summary>
    [Category(nameof(Category.ITU)), Name("UHDTV (2020)")]
    [Description("")]
    public static ColorProfile UHDTV_2020
        => new(Illuminant2.D65, new(0.708, 0.292), new(0.170, 0.797), new(0.131, 0.046), new Compression(1 / 0.45, CIE.Alpha - 1, CIE.Beta, 4.5, CIE.BetaInverse));

    /// <summary>
    /// <b>UHDTV</b> (2100)
    /// <para><i>ITU-R BT.2100</i> (Rec. 2100)</para>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// </summary>
    [Category(nameof(Category.ITU)), Name("UHDTV (2100)")]
    [Description("")]
    public static ColorProfile UHDTV_2100
        => new(Illuminant2.D65, new(0.708, 0.292), new(0.170, 0.797), new(0.131, 0.046), new PQCompression() /*|| new GammaLogCompression()*/);

    #endregion

    /// <see cref="Category.Holmes"/>
    #region

    /// <summary>Ekta Space PS5</summary><remarks><see cref="Illuminant2.D50"/></remarks>
    [Category(nameof(Category.Holmes)), Name("Ekta Space PS5")]
    [Description("Developed for high quality storage of image data from scans of transparencies.")]
    public static ColorProfile EktaSpacePS5
        => new(Illuminant2.D50, new(0.695, 0.305), new(0.260, 0.700), new(0.110, 0.005), new GammaCompression(2.2));

    #endregion

    /// <see cref="Category.Kodak"/>
    #region

    /// <summary>RIMM</summary><remarks><see cref="Illuminant2.D50"/></remarks>
    [Category(nameof(Category.Kodak)), Name("RIMM")]
    [Description("")]
    public static ColorProfile RIMM
        => new(Illuminant2.D50, new(0.7347, 0.2653), new(0.1596, 0.8404), new(0.0366, 0.0001), new Compression(20 / 9, 0.099, 0.0018, 5.5, 0.099));

    /// <summary>ROMM (ProPhoto)</summary><remarks><see cref="Illuminant2.D50"/></remarks>
    [Category(nameof(Category.Kodak)), Name("ROMM (ProPhoto)")]
    [Description("Offers an especially large gamut designed for use with photographic output in mind.")]
    public static ColorProfile ROMM
        => new(Illuminant2.D50, new(0.7347, 0.2653), new(0.1596, 0.8404), new(0.0366, 0.0001), new Compression(9 / 5, 0, 0.001953125, 16, 0.031248));

    #endregion

    /// <see cref="Category.Lindbloom"/>
    #region

    /// <summary>
    /// <b>Beta RGB</b>
    /// <para><see cref="Illuminant2.D50"/></para>
    /// <para>An optimized capture, archiving, and editing space for high-end digital imaging applications.</para>
    /// </summary>
    [Category(nameof(Category.Lindbloom)), Name("Beta RGB")]
    [Description("An optimized capture, archiving, and editing space for high-end digital imaging applications.")]
    public static ColorProfile BetaRGB
        => new(Illuminant2.D50, new(0.6888, 0.3112), new(0.1986, 0.7551), new(0.1265, 0.0352), new GammaCompression(2.2));

    #endregion

    /// <see cref="Category.NTSC"/>
    #region

    /// <summary>
    /// <b>NTSC-J</b>
    /// <para><see cref="Illuminant2.D93"/></para>
    /// </summary>
    [Category(nameof(Category.NTSC)), Name("NTSC-J")]
    [Description("")]
    [NotAccurate]
    public static ColorProfile NTSC_J
        => new(Illuminant2.D93, new(0.630, 0.340), new(0.310, 0.595), new(0.155, 0.070), ColorProfile.sRGBCompression);

    /// <summary>
    /// <b>NTSC-FCC</b>
    /// <para><see cref="Illuminant2.C"/></para>
    /// </summary>
    [Category(nameof(Category.NTSC)), Name("NTSC-FCC")]
    [Description("")]
    public static ColorProfile NTSC_FCC
        => new(Illuminant2.C, new(0.670, 0.330), new(0.210, 0.710), new(0.140, 0.080), new GammaCompression(2.5));

    /// <summary>
    /// <b>NTSC-RGB</b>
    /// <para><see cref="Illuminant2.C"/></para>
    /// </summary>
    [Category(nameof(Category.NTSC)), Name("NTSC-RGB")]
    [Description("")]
    public static ColorProfile NTSC_RGB
        => new(Illuminant2.C, new(0.670, 0.330), new(0.210, 0.710), new(0.140, 0.080), new GammaCompression(2.2));

    /// <summary>
    /// <b>NTSC-SMPTE (C)</b>
    /// <para><i>SMPTE RP 145 (C), 170M, 240M</i> (1987)</para>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// <para>Rec. 601 (525 lines)...?</para>
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/NTSC#SMPTE_C</remarks>
    [Category(nameof(Category.NTSC)), Name("NTSC-SMPTE (C)")]
    [Description("")]
    public static ColorProfile NTSC_SMPTE_C
        => new(Illuminant2.D65, new(0.630, 0.340), new(0.310, 0.595), new(0.155, 0.070), new Compression(20 / 9, 0.1115, 0.0057, 4, 0.0228));

    #endregion

    /// <see cref="Category.PAL"/>
    #region

    /// <summary>
    /// <b>PAL-M</b>
    /// <para><i>BT.470-6</i> (1972)</para>
    /// <para><see cref="Illuminant2.C"/></para>
    /// </summary>
    [Category(nameof(Category.PAL)), Name("PAL-M")]
    [Description("")]
    public static ColorProfile PAL_M
        => new(Illuminant2.C, new(0.670, 0.330), new(0.210, 0.710), new(0.140, 0.080), new GammaCompression(2.2));

    /// <summary>
    /// <b>PAL/SECAM</b>
    /// <para><i>EBU 3213-E, ITU-R BT.470/601 (B/G)</i> (1970)</para>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// <para>Rec. 601 (625 lines)...?</para>
    /// Gamma: <see href="https://github.com/tompazourek/Colourful/">Colourful</see> = 2.2/<see href="https://en.wikipedia.org/wiki/RGB_color_spaces">Wikipedia</see> = 2.8.
    /// </summary>
    [Category(nameof(Category.PAL)), Name("PAL/SECAM")]
    [Description("")]
    public static ColorProfile PAL_SECAM
        => new(Illuminant2.D65, new(0.640, 0.330), new(0.290, 0.600), new(0.150, 0.060), new GammaCompression(2.8));

    #endregion

    /// <see cref="Category.Microsoft"/>
    #region

    /// <summary>
    /// <b>scRGB</b>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// <para>Uses the same color primaries and white/black points as <see cref="sRGB"/>, but allows coordinates below zero and greater than one.</para>
    /// </summary>
    [Category(nameof(Category.Microsoft)), Name("scRGB"), NotComplete]
    [Description("Uses the same color primaries and white/black points as sRGB, but allows coordinates below zero and greater than one.")]
    public static ColorProfile scRGB
        => new(ColorProfile.sRGBWhite, ColorProfile.sRGBPrimary, ColorProfile.sRGBCompression);

    /// <summary>
    /// <b>sRGB</b>
    /// <para><see cref="Illuminant2.D65"/></para>
    /// <para>Developed as a color standard designed primarily for office, home, and web users.</para>
    /// </summary>
    [Category(nameof(Category.Microsoft)), Name("sRGB")]
    [Description("Developed as a color standard designed primarily for office, home, and web users.")]
    public static ColorProfile sRGB
        => new(ColorProfile.sRGBWhite, ColorProfile.sRGBPrimary, ColorProfile.sRGBCompression);

    #endregion

    /// <see cref="Category.Radius"/>
    #region

    /// <summary>
    /// <b>ColorMatch RGB</b>
    /// <para><see cref="Illuminant2.D50"/></para>
    /// </summary>
    [Category(nameof(Category.Radius)), Name("ColorMatch RGB")]
    [Description("")]
    public static ColorProfile ColorMatchRGB
        => new(Illuminant2.D50, new(0.630, 0.340), new(0.295, 0.605), new(0.150, 0.075), new GammaCompression(1.8));

    #endregion
}
#pragma warning restore