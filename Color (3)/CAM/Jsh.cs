using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Jsh</b> (<see cref="CAM02"/>)
/// <para><i>Lightness (J), Saturation (s), Hue (h)</i></para>
/// <para>A model specified by 'CAM02' that defines color as having lightness (J), saturation (S), and hue (h).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Jsh"/></para>
/// </summary>
[ColorOf(typeof(CAM02<>))]
[Component(100, '%', "J", "Lightness")]
[Component(100, '%', "s", "Saturation")]
[Component(360, '°', "h", "Hue")]
[Description("A model specified by 'CAM02' that defines color as having lightness (J), saturation (S), and hue (h).")]
[NotAccurate]
[Refactor]
public record class Jsh(double J, double S, double H)
    : CAM02<Jsh>(J, S, H), IColor3<Jsh, double>, System.Numerics.IMinMaxValue<Jsh>
{
    public static Jsh MaxValue => new(100, 100, 360);

    public static Jsh MinValue => new(0);

    public override double J { get => X; set => X = value; }

    public override double s { get => Y; set => Y = value; }

    public override double Q { get; set; }

    public override double C { get; set; }

    public override double M { get; set; }

    public Jsh() : this(default, default, default) { }

    public Jsh(double jsh) : this(jsh, jsh, jsh) { }

    public Jsh(IVector3<double> jsh) : this(jsh.X, jsh.Y, jsh.Z) { }

    /// <summary><see cref="XYZ"/> ⇒ <see cref="Jsh"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
    {
        var result = From<Jsh>(input, profile);
        XYZ = new(result.J, result.s, result.h);
    }
}