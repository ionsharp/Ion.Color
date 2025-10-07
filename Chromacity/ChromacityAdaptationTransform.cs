using Ion.Numeral;

namespace Ion.Colors;

public static class ChromacityAdaptationTransform
{
    /// <remarks>Unclear what this is for...</remarks>
    public static readonly Matrix3x3<double> XYZScaling = new(1, 1, 1, Matrix3x3Fill.DiagonalRight);

    /// <summary>Von Kries/Hunt-Pointer-Estevez (adjusted for equal energy).</summary>
    [Description("Von Kries/Hunt-Pointer-Estevez (adjusted for equal energy).")]
    public static Matrix3x3<double> VonKries => new
    (
        0.38971, 0.68898, -0.07868,
       -0.22981, 1.18340, 0.04641,
        0.00000, 0.00000, 1.00000
    );

    /// <summary>Von Kries/Hunt-Pointer-Estevez (adjusted for D65).</summary>
    [Description("Von Kries/Hunt-Pointer-Estevez (adjusted for D65).")]
    public static Matrix3x3<double> VonKriesAdjusted => new
    (
         0.40020, 0.70760, -0.0808,
        -0.22630, 1.16530, 0.0457,
         0.00000, 0.00000, 0.9182
    );

    /// <summary>Used in <see cref="CAT97"/>.</summary>
    [Description("Used in CAT-97.")]
    public static Matrix3x3<double> Bradford => new
    (
         0.8951, 0.2664, -0.1614,
        -0.7502, 1.7135, 0.0367,
         0.0389, -0.0686, 1.0296
    );

    /// <summary>Spectral-sharpened Bradford.</summary>
    [Description("Spectral-sharpened Bradford.")]
    public static Matrix3x3<double> BradfordSharp => new
    (
         1.2694, -0.0988, -0.1706,
        -0.8364, 1.8006, 0.0357,
         0.0297, -0.0315, 1.0018
    );

    /// <summary>CAT-97 chromatic adaptation transform (1997).</summary>
    [Description("CMC CAT-97 chromatic adaptation transform (1997).")]
    public static Matrix3x3<double> CAT97 => new
    (
         0.8562, 0.3372, -0.1934,
        -0.8360, 1.8327, 0.0033,
         0.0357, -0.00469, 1.0112
    );

    /// <summary>CAT-00 chromatic adaptation transform fitted from all available color data sets (CMCCAT2000).</summary>
    [Description("CMC CAT-00 chromatic adaptation transform fitted from all available color data sets (2000).")]
    public static Matrix3x3<double> CAT00 => new
    (
         0.7982, 0.3389, -0.1371,
        -0.5918, 1.5512, 0.0406,
         0.0008, 0.0239, 0.9753
    );

    /// <summary>CAT-02 chromatic adaptation transform optimized for minimizing L*a*b* differences (2002).</summary>
    [Description("CMC CAT-02 chromatic adaptation transform optimized for minimizing L*a*b* differences (2002).")]
    public static Matrix3x3<double> CAT02 => new
    (
         0.7328, 0.4296, -0.1624,
        -0.7036, 1.6975, 0.0061,
         0.0030, 0.0136, 0.9834
    );
}