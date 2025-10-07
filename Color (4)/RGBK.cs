using Ion.Numeral;
using System;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <b>Red (R), Green (G), Blue (B), Black (K)</b>
/// <para>An additive model where the primary colors are added with black.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="RGBK"/></para>
/// </summary>
[ColorOf<Lrgb>]
[Component(byte.MaxValue, '%', "R", "Red")]
[Component(byte.MaxValue, '%', "G", "Green")]
[Component(byte.MaxValue, '%', "B", "Blue")]
[Component(byte.MaxValue, '%', "K", "Black")]
[ComponentGroup(ComponentGroup.RB | ComponentGroup.RGB)]
[Description("An additive model where the primary colors are added with black.")]
public sealed record class RGBK(byte R, byte G, byte B, byte K)
    : Color4<RGBK, byte>(R, G, B, K), IColor4<RGBK, byte>, System.Numerics.IMinMaxValue<RGBK>
{
    public static RGBK MaxValue => new(byte.MaxValue);

    public static RGBK MinValue => new(byte.MinValue);

    public RGBK() : this(default, default, default, default) { }

    public RGBK(byte rgbk) : this(rgbk, rgbk, rgbk, rgbk) { }

    public RGBK(byte r, byte g, byte b) : this(r, g, b, default) { }

    public RGBK(in IVector3<byte> rgb) : this(rgb.X, rgb.Y, rgb.Z, default) { }

    public RGBK(in IVector4<byte> rgbk) : this(rgbk.X, rgbk.Y, rgbk.Z, rgbk.W) { }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="RGBK"/></summary>
    public override void From(in Lrgb i, ColorProfile profile)
    {
        var k0 = 1.0 - Max(i.X, Max(i.Y, i.Z));
        var k1 = 1.0 - k0;

        var r = (1 - i.X - k0) / k1;
        var g = (1 - i.Y - k0) / k1;
        var b = (1 - i.Z - k0) / k1;

        XYZW = new Vector4<Double1>(1 - r.NaN(0), 1 - g.NaN(0), 1 - b.NaN(0), k0).Denormalize(MinValue, MaxValue);
    }

    /// <summary><see cref="RGBK"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        var result = XYZW.Normalize(MinValue, MaxValue);
        var r = result.X * (1.0 - result.W);
        var g = result.Y * (1.0 - result.W);
        var b = result.Z * (1.0 - result.W);
        return new(r, g, b);
    }
}