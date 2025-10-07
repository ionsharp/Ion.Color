using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>JCh</b> (<see cref="CAM02"/>)
/// <para><i>Lightness (J), Chroma (C), Hue (h)</i></para>
/// <para>A model specified by 'CAM02' that defines color as having lightness (J), chroma (C), and hue (h).</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="JCh"/></para>
/// </summary>
[ColorOf(typeof(CAM02<>))]
[Component(100, '%', "J", "Lightness")]
[Component(100, '%', "C", "Chroma")]
[Component(360, '°', "h", "Hue")]
[Description("A model specified by 'CAM02' that defines color as having lightness (J), chroma (C), and hue (h).")]
[NotAccurate]
[Refactor]
public record class JCh(double J, double C, double H)
    : CAM02<JCh>(J, C, H), IColor3<JCh, double>, System.Numerics.IMinMaxValue<JCh>
{
    public static JCh MaxValue => new(100, 100, 360);

    public static JCh MinValue => new(0);

    public override double J { get => X; set => X = value; }

    public override double C { get => Y; set => Y = value; }

    public override double Q { get; set; }

    public override double M { get; set; }

    public override double s { get; set; }

    public JCh() : this(default, default, default) { }

    public JCh(double jch) : this(jch, jch, jch) { }

    public JCh(IVector3<double> jch) : this(jch.X, jch.Y, jch.Z) { }

    /// <summary><see cref="XYZ"/> ⇒ <see cref="JCh"/></summary>
    public override void From(in XYZ input, ColorProfile profile)
    {
        var result = From<JCh>(input, profile);
        XYZ = new(result.J, result.C, result.h);
    }
}