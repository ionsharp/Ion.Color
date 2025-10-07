using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Hue (H), Whiteness (W), Blackness (B)</b>
/// <para>A model derived from 'Labk' that defines color as having hue (H), whiteness (W), and blackness (B).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Labk"/> ⇒ <see cref="Labkwb"/></para>
/// </summary>
/// <remarks>https://colour.readthedocs.io/en/develop/_modules/colour/models/oklab.html</remarks>
[ColorOf<Labk>]
[Component(360, '°', "H", "Hue")]
[Component(100, '%', "W", "Whiteness")]
[Component(100, '%', "B", "Blackness")]
[ComponentGroup(ComponentGroup.H | ComponentGroup.WB)]
[Description("A model derived from 'Labk' that defines color as having hue (H), whiteness (W), and blackness (B).")]
[Hide]
public record class Labkwb(double H, double W, double B)
    : Color3<Labkwb, double, Labk>(H, W, B), IColor3<Labkwb, double>, System.Numerics.IMinMaxValue<Labkwb>
{
    public static Labkwb MaxValue => new(360, 100, 100);

    public static Labkwb MinValue => new(0);

    public Labkwb() : this(default, default, default) { }

    public Labkwb(double hwb) : this(hwb, hwb, hwb) { }

    public Labkwb(IVector3<double> hwb) : this(hwb.X, hwb.Y, hwb.Z) { }

    /// <summary><see cref="Labk"/> ⇒ <see cref="Labkwb"/></summary>
    [NotComplete]
    public override void From(in Labk input, ColorProfile profile) { }

    /// <summary><see cref="Labkwb"/> ⇒ <see cref="Labk"/></summary>
    [NotComplete]
    public override void To(out Labk result, ColorProfile profile) => result = new();
}