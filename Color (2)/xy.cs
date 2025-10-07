using Ion.Numeral;
using System;

namespace Ion.Colors;


/// <summary>
/// <b>Chroma (x), Chroma (y)</b>
/// <para>A color with two <b>Chroma</b> components.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="xyY"/> ⇒ <see cref="XY"/></para>
/// </summary>
[Component(1, "x")]
[Component(1, "y")]
[Hide]
public record class XY(Double1 X, Double1 Y)
    : Color2<XY, Double1, xyY>(X, Y), IColor2<XY, Double1>, System.Numerics.IMinMaxValue<XY>
{
    public static XY MaxValue => new(Double1.MaxValue);

    public static XY MinValue => new(Double1.MinValue);

    public XY() : this(default, default) { }

    public XY(Double1 xy) : this(xy, xy) { }

    public XY(in IVector2<Double1> xy) : this(xy.X, xy.Y) { }

    public static explicit operator XY(Vector2 input) => IColor.New<XY>(input);

    public static explicit operator XY(xyY input) => IColor.New<XY>(input.XY);

    /// <see cref="IConvert{xyY}"/>

    /// <summary><see cref="xyY"/> ⇒ <see cref="XY"/></summary>
    public override void From(in xyY i, ColorProfile profile)
        => XY = i.XY.NewType(j => new Double1(j));

    /// <summary><see cref="XY"/> ⇒ <see cref="xyY"/></summary>
    public override void To(out xyY i, ColorProfile profile)
        => i = new(X, Y);
}