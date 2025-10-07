using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>QMh</b> (<see cref="CAM02"/>)
/// <para><i>Brightness (Q), Colorfulness (M), Hue (h)</i></para>
/// <para>A model specified by 'CAM02' that defines color as having brightness (Q), colorfulness (M), and hue (h).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="QMh"/></para>
/// </summary>
[ColorOf(typeof(CAM02<>))]
[Component(100, '%', "Q", "Brightness")]
[Component(100, '%', "M", "Colorfulness")]
[Component(360, '°', "h", "Hue")]
[Description("A model specified by 'CAM02' that defines color as having brightness (Q), colorfulness (M), and hue (h).")]
[NotAccurate]
[Refactor]
public record class QMh(double Q, double M, double H)
    : CAM02<QMh>(Q, M, H), IColor3<QMh, double>, System.Numerics.IMinMaxValue<QMh>
{
    public static QMh MaxValue => new(100, 100, 360);

    public static QMh MinValue => new(0);

    public override double Q { get => X; set => X = value; }

    public override double M { get => Y; set => Y = value; }

    public override double J { get; set; }

    public override double C { get; set; }

    public override double s { get; set; }

    public QMh() : this(default, default, default) { }

    public QMh(double qmh) : this(qmh, qmh, qmh) { }

    public QMh(IVector3<double> qmh) : this(qmh.X, qmh.Y, qmh.Z) { }

    /// <summary><see cref="XYZ"/> ⇒ <see cref="QMh"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
    {
        var result = From<QMh>(input, profile);
        XYZ = new(result.Q, result.M, result.h);
    }
}