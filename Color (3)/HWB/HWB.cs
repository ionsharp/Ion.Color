using Ion.Numeral;
using System;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <b>Hue (H), Whiteness (W), Blackness (B)</b>
/// <para>A color with hue (H), whiteness (W), and blackness (B).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <b>TOther</b> ⇒ <see cref="HWB{TSelf, TOther}"/></para>
/// </summary>
/// <remarks>https://drafts.csswg.org/css-color/#the-hwb-notation</remarks>
[ColorOf<Lrgb>]
[Component(360, '°', "H", "Hue")]
[Component(100, '%', "W", "Whiteness")]
[Component(100, '%', "B", "Blackness")]
[ComponentGroup(ComponentGroup.H | ComponentGroup.WB)]
public abstract record class HWB<TSelf, TOther>(double H, double W, double B)
    : Color3<TSelf, double>(H, W, B)
    where TSelf : IColor3<TSelf, double>, System.Numerics.IMinMaxValue<TSelf>
    where TOther : IColor3, new()
{
    public static Vector3 MaxValue => new(360, 100, 100);

    public static Vector3 MinValue => new(0);

    /// <summary><see cref="Lrgb"/> ⇒ <b>TOther</b> ⇒ <see cref="HWB{TSelf, TOther}"/></summary>
    public sealed override void From(in Lrgb input, ColorProfile profile)
    {
        var other = new TOther();
        other.From(input, profile);

        Double1
            white = Min(input.X, Min(input.Y, input.Z)),
            black = 1 - Max(input.X, Max(input.Y, input.Z));

        XYZ = (other.X, white.Denormalize(MinValue.Y, MaxValue.Y), black.Denormalize(MinValue.Z, MaxValue.Z));
    }

    /// <summary><see cref="HWB{TSelf, TOther}"/> ⇒ <b>TOther</b> ⇒ <see cref="Lrgb"/></summary>
    public sealed override Lrgb To(ColorProfile profile)
    {
        var white = Y.Normalize(MinValue.Y, MaxValue.Y);
        var black = Z.Normalize(MinValue.Y, MaxValue.Y);

        if (white + black >= 1)
        {
            var gray = white / (white + black);
            return new(gray);
        }

        var other = new TOther();
        other.XYZ = (X.ToDouble(), 100D, 100D);
        other.To(out Lrgb rgb, profile);

        rgb.X *= (1 - white - black);
        rgb.X += white;

        rgb.Y *= (1 - white - black);
        rgb.Y += white;

        rgb.Z *= (1 - white - black);
        rgb.Z += white;
        return rgb;
    }
}