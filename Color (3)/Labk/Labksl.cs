using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Hue (H), Saturation (S), Lightness (L)</b>
/// <para>A model derived from 'Labk' that defines color as having hue (H), saturation (S), and lightness (L).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Labk"/> ⇒ <see cref="Labksl"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>Okhsd</item>
/// <item>Okhsi</item>
/// </list>
/// </summary>
/// <remarks>https://colour.readthedocs.io/en/develop/_modules/colour/models/oklab.html</remarks>
[ColorOf<Labk>]
[Component(360, '°', "H", "Hue")]
[Component(100, '%', "S", "Saturation")]
[Component(100, '%', "L", "Lightness")]
[ComponentGroup(ComponentGroup.H | ComponentGroup.HS | ComponentGroup.Lightness | ComponentGroup.SL)]
[Description("A model derived from 'Labk' that defines color as having hue (H), saturation (S), and lightness (L).")]
[Hide]
public record class Labksl(double H, double S, double L)
    : Color3<Labksl, double, Labk>(H, S, L), IColor3<Labksl, double>, System.Numerics.IMinMaxValue<Labksl>
{
    public static Labksl MaxValue => new(360, 100, 100);

    public static Labksl MinValue => new(0);

    public Labksl() : this(default, default, default) { }

    public Labksl(double hsl) : this(hsl, hsl, hsl) { }

    public Labksl(IVector3<double> hsl) : this(hsl.X, hsl.Y, hsl.Z) { }

    /// <summary><see cref="Labk"/> ⇒ <see cref="Labksl"/></summary>
    [NotComplete]
    public override void From(in Labk input, ColorProfile profile) { }

    /// <summary><see cref="Labksl"/> ⇒ <see cref="Labk"/></summary>
    [NotComplete]
    public override void To(out Labk result, ColorProfile profile) => result = new();
}