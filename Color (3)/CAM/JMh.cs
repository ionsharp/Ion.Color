using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>JMh</b> (<see cref="CAM02"/>)
/// <para><i>Lightness (J), Colorfulness (M), Hue (h)</i></para>
/// <para>A model specified by 'CAM02' that defines color as having lightness (J), colorfulness (M), and hue (h).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="JMh"/></para>
/// </summary>
[ColorOf(typeof(CAM02<>))]
[Component(100, '%', "J", "Lightness")]
[Component(100, '%', "M", "Colorfulness")]
[Component(360, '°', "h", "Hue")]
[Description("A model specified by 'CAM02' that defines color as having lightness (J), colorfulness (M), and hue (h).")]
[NotAccurate]
[Refactor]
public record class JMh(double J, double M, double H)
    : CAM02<JMh>(J, M, H), IColor3<JMh, double>, System.Numerics.IMinMaxValue<JMh>
{
    public static JMh MaxValue => new(100, 100, 360);

    public static JMh MinValue => new(0);

    public override double J { get => X; set => X = value; }

    public override double M { get => Y; set => Y = value; }

    public override double Q { get; set; }

    public override double C { get; set; }

    public override double s { get; set; }

    public JMh() : this(default, default, default) { }

    public JMh(double jmh) : this(jmh, jmh, jmh) { }

    public JMh(IVector3<double> jmh) : this(jmh.X, jmh.Y, jmh.Z) { }

    /// <summary><see cref="XYZ"/> ⇒ <see cref="JMh"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
    {
        var result = From<JMh>(input, profile);
        XYZ = new(result.J, result.M, result.h);
    }
}