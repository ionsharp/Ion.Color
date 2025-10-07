using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Hue (H), Whiteness (W), Blackness (B)</b>
/// <para>A color based on <see cref="HSL"/> with hue (H), whiteness (W), and blackness (B).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="HSL"/> ⇒ <see cref="HWBsl"/></para>
/// </summary>
/// <inheritdoc/>
[ColorOf(typeof(HWB<,>))]
[ComponentGroup(ComponentGroup.H | ComponentGroup.WB)]
[Description("A model that defines color as having hue (H), whiteness (W), and blackness (B).")]
public record class HWBsl(double H, double W, double B)
    : HWB<HWBsl, HSL>(H, W, B), IColor3<HWBsl, double>, System.Numerics.IMinMaxValue<HWBsl>
{
    new public static HWBsl MaxValue => new(360, 100, 100);

    new public static HWBsl MinValue => new(0);

    public HWBsl() : this(default, default, default) { }

    public HWBsl(double hwb) : this(hwb, hwb, hwb) { }

    public HWBsl(IVector3<double> hwb) : this(hwb.X, hwb.Y, hwb.Z) { }
}