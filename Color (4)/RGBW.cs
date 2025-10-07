using Ion.Numeral;
using System;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <b>Red (R), Green (G), Blue (B), White (W)</b>
/// <para>An additive model where the primary colors are added with white.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="RGBW"/></para>
/// </summary>
/// <remarks>
/// <para>https://andi-siess.de/rgb-to-color-temperature/</para>
/// <para>https://stackoverflow.com/questions/21117842/converting-an-rgbw-color-to-a-standard-rgb-hsb-representation</para>
/// <para>https://stackoverflow.com/questions/40312216/converting-rgb-to-rgbw</para>
/// </remarks>
[ColorOf<Lrgb>]
[Component(byte.MaxValue, "R", "Red")]
[Component(byte.MaxValue, "G", "Green")]
[Component(byte.MaxValue, "B", "Blue")]
[Component(byte.MaxValue, "W", "White")]
[ComponentGroup(ComponentGroup.RB | ComponentGroup.RGB)]
[Description("An additive model where the primary colors are added with white.")]
public sealed record class RGBW(byte X, byte Y, byte Z, byte W)
    : Color4<RGBW, byte>(X, Y, Z, W), IColor4<RGBW, byte>, System.Numerics.IMinMaxValue<RGBW>
{
    public static RGBW MaxValue => new(byte.MaxValue);

    public static RGBW MinValue => new(byte.MinValue);

    public RGBW() : this(default, default, default, default) { }

    public RGBW(byte rgbw) : this(rgbw, rgbw, rgbw, rgbw) { }

    public RGBW(byte r, byte g, byte b) : this(r, g, b, default) { }

    public RGBW(in IVector3<byte> rgb) : this(rgb.X, rgb.Y, rgb.Z, default) { }

    public RGBW(in IVector4<byte> rgbw) : this(rgbw.X, rgbw.Y, rgbw.Z, rgbw.W) { }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="RGBW"/></summary>
    public override void From(in Lrgb i, ColorProfile profile)
    {
        var r = i.X; var g = i.Y; var b = i.Z;
        var w = Min(r, Min(g, b));

        r -= w; r /= 1 - w;
        g -= w; g /= 1 - w;
        b -= w; b /= 1 - w;

        XYZW = new Vector4<Double1>(r, g, b, w).Denormalize(MinValue, MaxValue);
    }

    /// <summary><see cref="RGBW"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        var result = XYZW.Normalize(MinValue, MaxValue);
        Double1 r = result.X, g = result.Y, b = result.Z, w = result.W;
        r *= (1 - w); r += w;
        g *= (1 - w); g += w;
        b *= (1 - w); b += w;
        return new(r, g, b);
    }
}