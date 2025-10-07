using Ion.Numeral;
using System;
using System.Collections.Generic;

namespace Ion.Colors;

/// <summary>
/// <para><b>Red (R), Green (G), Blue (B)</b></para>
/// 
/// <para>An additive color model in which <b>Red</b>, <b>Green</b>, and <b>Blue</b> <i>primary</i> colors are added together.</para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>CIERGB</item>
/// </list>
/// 
/// <i>Author</i>
/// <list type="bullet">
/// <item><see cref="CIE"/> (1931)</item>
/// </list>
/// </summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
[ColorOf<RGB>]
[Component(byte.MaxValue, "R", "Red")]
[Component(byte.MaxValue, "G", "Green")]
[Component(byte.MaxValue, "B", "Blue")]
[ComponentGroup(ComponentGroup.RB | ComponentGroup.RGB)]
[Description("An additive model where the primary colors are added together.")]
public record class RGB(byte R, byte G, byte B)
    : Color3<RGB, byte>(R, G, B), IColor3<RGB, byte>, System.Numerics.IMinMaxValue<RGB>
{
    /// <see cref="Region.Property"/>

    public static RGB MaxValue => new(byte.MaxValue);

    public static RGB MinValue => new(byte.MinValue);

    /// <see cref="Region.Constructor"/>

    /// <summary>
    /// Get new instance with all channels 0.
    /// </summary>
    public RGB() : this(0) { }

    /// <summary>
    /// Get new instance with all channels equal (gray).
    /// </summary>
    public RGB(byte rgb) : this(rgb, rgb, rgb) { }

    /// <summary>
    /// Get new instance with given red and green channel.
    /// </summary>
    public RGB(byte r, byte g) : this(r, g, byte.MinValue) { }

    /// <summary>
    /// Get new instance from given <see cref="IVector2"/>.
    /// </summary>
    public RGB(in IVector2<byte> rg) : this(rg.X, rg.Y) { }

    /// <summary>
    /// Get new instance from given <see cref="IVector3"/>.
    /// </summary>
    public RGB(in IVector3<byte> rgb) : this(rgb.X, rgb.Y, rgb.Z) { }

    /// <see cref="Region.Operator"/>

    public static implicit operator Vector2<byte>(in RGB i) => new(i);

    public static implicit operator Vector3<byte>(in RGB i) => new(i);

    public static implicit operator Vector4<byte>(in RGB i) => new(i);

    public static implicit operator ByteVector2(in RGB i) => new(i);

    public static implicit operator ByteVector3(in RGB i) => new(i);

    public static implicit operator ByteVector4(in RGB i) => new(i);

    public static implicit operator RGB(Vector2<byte> i) => new(i as IVector2<byte>);

    public static implicit operator RGB(Vector3<byte> i) => new(i as IVector3<byte>);

    public static implicit operator RGB(Vector4<byte> i) => new(i as IVector4<byte>);

    public static implicit operator RGB(ByteVector2 i) => new(i as IVector2<byte>);

    public static implicit operator RGB(ByteVector3 i) => new(i as IVector3<byte>);

    public static implicit operator RGB(ByteVector4 i) => new(i as IVector4<byte>);

    /// <see cref="Region.Method"/>

    /// <summary><see cref="RGB"/> (0) > <see cref="XYZ"/> (0) > <see cref="LMS"/> (0) > <see cref="LMS"/> (1) > <see cref="XYZ"/> (1) > <see cref="RGB"/> (1)</summary>
    public override void Adapt(ColorProfile source, ColorProfile target) => XYZ = Adapt(this, source, target);

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="RGB"/></summary>
    public override void From(in Lrgb input, ColorProfile profile)
    {
        var result = input.XYZ.New((i, j) => profile.Compression.Transfer(j)).NewType(i => new Double1(i));
        XYZ = result.Denormalize<byte>();
    }

    /// <summary><see cref="RGB"/> ⇒ <see cref="RGB"/></summary>
    public override void From(in RGB input, ColorProfile profile) => XYZ = input.XYZ;

    /// <summary><see cref="RGB"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        var result = XYZ.Normalize().NewType((i, j) => profile.Compression?.TransferInverse(j) ?? 0);
        return new(result);
    }

    /// <summary><see cref="RGB"/> ⇒ <see cref="RGB"/></summary>
    public override void To(out RGB result, ColorProfile profile) => result = new(this);
}