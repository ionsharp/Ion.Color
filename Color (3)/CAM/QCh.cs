using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>QCh</b> (<see cref="CAM02"/>)
/// <para><i>Brightness (Q), Chroma (C), Hue (h)</i></para>
/// <para>A model specified by 'CAM02' that defines color as having brightness (Q), chroma (C), and hue (h).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="QCh"/></para>
/// </summary>
[ColorOf(typeof(CAM02<>))]
[Component(100, '%', "Q", "Brightness")]
[Component(100, '%', "C", "Chroma")]
[Component(360, '°', "h", "Hue")]
[Description("A model specified by 'CAM02' that defines color as having brightness (Q), chroma (C), and hue (h).")]
[NotAccurate]
[Refactor]
public record class QCh(double Q, double C, double H)
    : CAM02<QCh>(Q, C, H), IColor3<QCh, double>, System.Numerics.IMinMaxValue<QCh>
{
    public static QCh MaxValue => new(100, 100, 360);

    public static QCh MinValue => new(0);

    public override double Q { get => X; set => X = value; }

    public override double C { get => Y; set => Y = value; }

    public override double J { get; set; }

    public override double M { get; set; }

    public override double s { get; set; }

    public QCh() : this(default, default, default) { }

    public QCh(double qch) : this(qch, qch, qch) { }

    public QCh(IVector3<double> qch) : this(qch.X, qch.Y, qch.Z) { }

    /// <summary><see cref="XYZ"/> ⇒ <see cref="QCh"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
    {
        var result = From<QCh>(input, profile);
        XYZ = new(result.Q, result.C, result.h);
    }
}