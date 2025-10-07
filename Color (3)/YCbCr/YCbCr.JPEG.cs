using Ion.Collect;
using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Luminance (Y), Cb, Cr</b>
/// <para>A color model based on 'YCbCr' (Rec. 601) where all three components have the full 8-bit range of [0, 255].</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="YPbPr"/> ⇒ <see cref="YCbCr"/> ⇒ <see cref="JPEG"/></para>
/// </summary>
/// <remarks>https://github.com/colorjs/color-space/blob/master/jpeg.js</remarks>
[ColorOf<YCbCr>]
[Component(byte.MaxValue, "Y", "Luminance")]
[Component(byte.MaxValue, "Cb")]
[Component(byte.MaxValue, "Cr")]
[ComponentGroup(ComponentGroup.Luminance)]
[Description("A color model based on 'YCbCr' (Rec. 601) where all three components have the full 8-bit range of [0, 255].")]
public record class JPEG(byte Y, byte Cb, byte Cr)
    : Color3<JPEG, byte, YCbCr>(Y, Cb, Cr), IColor3<JPEG, byte>, System.Numerics.IMinMaxValue<JPEG>
{
    private const byte ByteHalf = 128;

    public static JPEG MaxValue => new(byte.MaxValue);

    public static JPEG MinValue => new(byte.MinValue);

    public static readonly Matrix3x3<double> RGB_YCbCr = new
    (
        1,  0.00000,  1.40200,
        1, -0.34414, -0.71414,
        1,  1.77200,  0.00000
    );

    public static readonly Matrix3x3<double> YCbCr_RGB = new
    (
         0.299000,  0.587000,  0.114000,
        -0.168736, -0.331264,  0.500000,
         0.500000, -0.418688, -0.081312
    );

    public JPEG() : this(default, default, default) { }

    public JPEG(byte ycbcr) : this(ycbcr, ycbcr, ycbcr) { }

    public JPEG(IVector3<byte> ycbcr) : this(ycbcr.X, ycbcr.Y, ycbcr.Z) { }

    /// <summary><see cref="YCbCr"/> ⇒ <see cref="JPEG"/></summary>
    public override void From(in YCbCr i, ColorProfile profile)
        => XYZ = YCbCr_RGB.Do(Operator.Multiply, i).Do(Operator.Add, new Vector3(0, ByteHalf, ByteHalf)).Clamp(byte.MaxValue).NewType(j => j.ToByte());

    /// <summary><see cref="JPEG"/> ⇒ <see cref="YCbCr"/></summary>
    public override void To(out YCbCr i, ColorProfile profile)
    {
        double y = X, cb = Y, cr = Z;

        var e = RGB_YCbCr[0, 0] * y +  RGB_YCbCr[0, 1] * cb              + RGB_YCbCr[0, 2] * (cr - ByteHalf);
        var f = RGB_YCbCr[1, 0] * y +  RGB_YCbCr[1, 1] * (cb - ByteHalf) + RGB_YCbCr[1, 2] * (cr - ByteHalf);
        var g = RGB_YCbCr[2, 0] * y +  RGB_YCbCr[2, 1] * (cb - ByteHalf) + RGB_YCbCr[2, 2] * cr;

        i = new(e, f, g);
    }
}