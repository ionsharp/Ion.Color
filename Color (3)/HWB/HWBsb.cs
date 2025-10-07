using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Hue (H), Whiteness (W), Blackness (B)</b>
/// <para>A color based on <see cref="HSB"/> with hue (H), whiteness (W), and blackness (B).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="HSB"/> ⇒ <see cref="HWBsb"/></para>
/// </summary>
/// <inheritdoc/>
[ColorOf(typeof(HWB<,>))]
[ComponentGroup(ComponentGroup.H | ComponentGroup.WB)]
[Description("A model that defines color as having hue (H), whiteness (W), and blackness (B).")]
public record class HWBsb(double H, double W, double B)
    : HWB<HWBsb, HSB>(H, W, B), IColor3<HWBsb, double>, System.Numerics.IMinMaxValue<HWBsb>
{
    new public static HWBsb MaxValue => new(360, 100, 100);

    new public static HWBsb MinValue => new(0);

    public HWBsb() : this(default, default, default) { }

    public HWBsb(double hwb) : this(hwb, hwb, hwb) { }

    public HWBsb(IVector3<double> hwb) : this(hwb.X, hwb.Y, hwb.Z) { }
}