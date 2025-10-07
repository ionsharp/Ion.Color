using Ion.Numeral;
using System;
using System.Globalization;

namespace Ion.Colors;

/// <summary>A standardized, device-independent format for reproducing color.</summary>
/// <remarks>https://en.wikipedia.org/wiki/Color_management</remarks>
[Description(Description)]
public readonly partial record struct ColorProfile(in Vector2 Chromacity, in Primary3 Primary, in ICompress Compression, in Matrix3x3<double> Adaptation = default, in CAM02Conditions Conditions = default)
    : IFormattable
{
    public const string Description = "A standardized, device-independent format for reproducing color.";

    public const string StringFormat = "Primary = {0}, White = {1}, Compression = {2}, Adaptation = {3}";

    public static ColorProfile Default => ColorProfiles.sRGB;

#pragma warning disable IDE1006
    public static ICompress
        sRGBCompression
        => new Compression(12 / 5, 1.055, 0.0031308, 12.92, 0.04045);

    public static Primary3
        sRGBPrimary
        => new(new(0.6400, 0.3300), new(0.3000, 0.6000), new(0.1500, 0.0600));

    public static Vector2
        sRGBWhite
        => Illuminant2.D65;
#pragma warning restore

    /// <summary><see cref="ColorProfiles.sRGB"/></summary>
    public static Matrix3x3<double> DefaultAdaptation => ChromacityAdaptationTransform.Bradford;

    /// <summary><see cref="ColorProfiles.sRGB"/></summary>
    public static ICompress DefaultCompression => sRGBCompression;

    /// <summary><see cref="ColorProfiles.sRGB"/></summary>
    public static Primary3 DefaultPrimary => sRGBPrimary;

    /// <summary></summary>
    [NotImplemented]
    public static CAM02Conditions DefaultConditions => new();

    /// <summary><see cref="ColorProfiles.sRGB"/></summary>
    public static Vector2 DefaultWhite => sRGBWhite;

    ///

    /// <summary>The matrix used to adapt a color when the input and output color profile differ.</summary>
    [Description("The matrix used to adapt a color when the input and output working profile differ.")]
    public Matrix3x3<double> Adaptation { get; } = Adaptation;

    /// <summary>The chromacity coordinates.</summary>
    [Description("The chromacity coordinates.")]
    public Vector2 Chromacity { get; } = Chromacity;

    [Description]
    public ICompress Compression { get; } = Compression;

    [Description("The viewing conditions in CAM02.")]
    public CAM02Conditions Conditions { get; } = Conditions;

    /// <summary>The red, green, and blue primary coordinates.</summary>
    [Description("The red, green, and blue primary coordinates.")]
    public Primary3 Primary { get; } = Primary;

    /// <summary>The point in the gamut where all colors combine to form white, providing reference for all other color.</summary>
    /// <remarks><see cref="White"/> = (<see cref="Vector3"/>)(<see cref="XYZ"/>)(<see cref="xyY"/>)(<see cref="XY"/>)<see cref="Chromacity"/></remarks>
    [Description("The point in the gamut where all colors combine to form white, providing reference for all other color.")]
    public Vector3 White { get; } = (XYZ)(xyY)(XY)Chromacity;

    /// <summary>Get a new instance.</summary>
    /// <remarks>See <see cref="DefaultAdaptation"/>, <see cref="DefaultCompression"/>, <see cref="DefaultConditions"/>, <see cref="DefaultPrimary"/>, and <see cref="DefaultWhite"/>.</remarks>
    public ColorProfile() : this(DefaultWhite, DefaultPrimary, DefaultCompression, DefaultAdaptation, DefaultConditions) { }

    /// <summary>Get a new instance.</summary>
    /// <remarks>See <see cref="DefaultAdaptation"/>, <see cref="DefaultCompression"/>, and <see cref="DefaultConditions"/>.</remarks>
    public ColorProfile(in Vector2 chromacity, in Vector2 pR, in Vector2 pG, in Vector2 pB, in ICompress compression = default, in Matrix3x3<double> adaptation = default, in CAM02Conditions conditions = default)
        : this(chromacity, new Primary3(pR, pG, pB), compression, adaptation, conditions) { }

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider)
        => StringFormat.F(Primary.ToString(format, provider), White.ToString(format, provider), Compression.ToString(format, provider), Adaptation.ToString(format, provider));
}