using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Hue (H), Saturation (S), Brightness (B)</b>
/// <para>A model derived from 'Labk' that defines color as having hue (H), saturation (S), and brightness (B).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Labk"/> ⇒ <see cref="Labksb"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>Okhsb</item>
/// </list>
/// </summary>
/// <remarks>https://colour.readthedocs.io/en/develop/_modules/colour/models/oklab.html</remarks>
[ColorOf<Labk>]
[Component(360, '°', "H", "Hue")]
[Component(100, '%', "S", "Saturation")]
[Component(100, '%', "B", "Brightness")]
[ComponentGroup(ComponentGroup.H | ComponentGroup.HS | ComponentGroup.SB)]
[Description("A model derived from 'Labk' that defines color as having hue (H), saturation (S), and brightness (B).")]
[Hide]
public record class Labksb(double H, double S, double B)
    : Color3<Labksb, double, Labk>(H, S, B), IColor3<Labksb, double>, System.Numerics.IMinMaxValue<Labksb>
{
    public static Labksb MaxValue => new(360, 100, 100);

    public static Labksb MinValue => new(0);

    public Labksb() : this(default, default, default) { }

    public Labksb(double hsb) : this(hsb, hsb, hsb) { }

    public Labksb(IVector3<double> hsb) : this(hsb.X, hsb.Y, hsb.Z) { }

    /// <summary><see cref="Labk"/> ⇒ <see cref="Labksb"/></summary>
    [NotComplete]
    public override void From(in Labk input, ColorProfile profile) { }

    /// <summary><see cref="Labksb"/> ⇒ <see cref="Labk"/></summary>
    [NotComplete]
    public override void To(out Labk result, ColorProfile profile) => result = new();
}