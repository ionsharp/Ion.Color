using Ion.Numeral;
using System;
using System.Collections.Generic;

namespace Ion.Colors;

/// <summary>
/// <para><b>Red (R), Green (G), Blue (B)</b></para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>LRGB</item>
/// <item>Linear RGB</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
[ColorOf<RGB>]
[Component(1, '%', "R", "Red")]
[Component(1, '%', "G", "Green")]
[Component(1, '%', "B", "Blue")]
[ComponentGroup(ComponentGroup.RB | ComponentGroup.RGB)]
[Hide]
public record class Lrgb(double R, double G, double B)
    : Color3<Lrgb, double>(R, G, B), IColor3<Lrgb, double>, System.Numerics.IMinMaxValue<Lrgb>
{
    public static Lrgb MaxValue => new(1);

    public static Lrgb MinValue => new(0);

    public Lrgb() : this(default, default, default) { }

    public Lrgb(double lrgb) : this(lrgb, lrgb, lrgb) { }

    public Lrgb(in IVector3<double> lrgb) : this(lrgb.X, lrgb.Y, lrgb.Z) { }

    public static implicit operator Vector3(in Lrgb i) => new(i);

    public static implicit operator Vector3<Double>(in Lrgb i) => new(i);

    public static implicit operator Vector3<Double1>(in Lrgb i) => new(i.NewType(j => new Double1(j)));

    public static implicit operator Lrgb(Vector3 i) => new(i as IVector3<double>);

    public static implicit operator Lrgb(Vector3<Double> i) => new(i as IVector3<double>);

    public static implicit operator Lrgb(Vector3<Double1> i) => new(i.New(j => j.ToDouble()));

    public override void From(in Lrgb i, ColorProfile profile) => XYZ = i;

    public override Lrgb To(ColorProfile profile) => new(this);
}