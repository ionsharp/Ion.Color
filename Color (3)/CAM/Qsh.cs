using Ion.Numeral;

namespace Ion.Colors;

/// <summary>
/// <b>Qsh</b> (<see cref="CAM02"/>)
/// <para><i>Brightness (Q), Saturation (s), Hue (h)</i></para>
/// <para>A model specified by 'CAM02' that defines color as having brightness (Q), saturation (s), and hue (h).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Qsh"/></para>
/// </summary>
[ColorOf(typeof(CAM02<>))]
[Component(100, '%', "Q", "Brightness")]
[Component(100, '%', "s", "Saturation")]
[Component(360, '°', "h", "Hue")]
[Description("A model specified by 'CAM02' that defines color as having brightness (Q), saturation (s), and hue (h).")]
[NotAccurate]
[Refactor]
public record class Qsh(double Q, double S, double H)
    : CAM02<Qsh>(Q, S, H), IColor3<Qsh, double>, System.Numerics.IMinMaxValue<Qsh>
{
    public static Qsh MaxValue => new(100, 100, 360);

    public static Qsh MinValue => new(0);

    public override double Q { get => X; set => X = value; }

    public override double s { get => Y; set => Y = value; }

    public override double J { get; set; }

    public override double C { get; set; }

    public override double M { get; set; }

    public Qsh() : this(default, default, default) { }

    public Qsh(double qsh) : this(qsh, qsh, qsh) { }

    public Qsh(IVector3<double> qsh) : this(qsh.X, qsh.Y, qsh.Z) { }

    /// <summary><see cref="XYZ"/> ⇒ <see cref="Qsh"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
    {
        var result = From<Qsh>(input, profile);
        XYZ = new(result.Q, result.s, result.h);
    }
}