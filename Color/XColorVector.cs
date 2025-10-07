using Ion.Numeral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ion.Colors;

public static class XColorVector
{
    #region Blend

    private static double BlendColorBurnf(double a, double b)
    => ((b == 0.0) ? b : Math.Max((1.0 - ((1.0 - a) / b)), 0.0));

    private static double BlendColorDodgef(double a, double b)
        => ((b == 1.0) ? b : Math.Min(a / (1.0 - b), 1.0));

    private static double BlendHardMixf(double a, double b)
        => BlendVividLightf(a, b) < 0.5 ? 0 : 1;

    private static double BlendReflectf(double a, double b)
        => (b == 1.0) ? b : Math.Min(a * a / (1.0 - b), 1.0);

    private static double BlendSoftLightf(double a, double b)
        => ((b < 0.5) ? (2.0 * a * b + a * a * (1.0 - 2.0 * b)) : (Math.Sqrt(a) * (2.0 * b - 1.0) + 2.0 * a * (1.0 - b)));

    private static double BlendVividLightf(double a, double b)
        => b < 0.5 ? BlendColorBurnf(a, 2.0 * b) : BlendColorDodgef(a, 2.0 * (b - 0.5));

    /// <summary><see cref="IVector3"/> + <see cref="IVector3"/> ⇒ <see cref="IVector3"/></summary>
    public static ByteVector3 Blend(this IVector3<byte> a, IVector3<byte> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default)
        => new ByteVector4(a).Blend(new ByteVector4(b), blendMode, amount, profile).XYZ;

    /// <summary><see cref="IVector3"/> + <see cref="IVector4"/> ⇒ <see cref="IVector3"/></summary>
    public static ByteVector3 Blend(this IVector3<byte> a, IVector4<byte> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default)
        => new ByteVector4(a).Blend(b, blendMode, amount, profile).XYZ;

    /// <summary><see cref="IVector4"/> + <see cref="IVector3"/> ⇒ <see cref="IVector4"/></summary>
    public static ByteVector4 Blend(this IVector4<byte> a, IVector3<byte> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default)
        => a.Blend(new ByteVector4(b), blendMode, amount, profile);

    /// <summary><see cref="IVector4"/> + <see cref="IVector4"/> ⇒ <see cref="IVector4"/></summary>
    public static ByteVector4 Blend(this IVector4<byte> a, IVector4<byte> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default)
    {
        var ba = System.Convert.ToByte(System.Convert.ToDouble(b.W) / 255D * amount * 255D);
        double a1 = a.W.Normalize(), r1 = a.X.Normalize(), g1 = a.Y.Normalize(), b1 = a.Z.Normalize();
        double a2 =  ba.Normalize(), r2 = b.X.Normalize(), g2 = b.Y.Normalize(), b2 = b.Z.Normalize();

        double w = a1, x = 0, y = 0, z = 0;

        HSB hsb1, hsb2;
        RGB rgb;

        switch (blendMode)
        {
            case BlendModes.Average:
                w = (a1 + a2) / 2;
                x = (r1 + r2) / 2;
                y = (g1 + g2) / 2;
                z = (b1 + b2) / 2;
                break;
            case BlendModes.Color:
                hsb1 = new HSB();
                hsb1.From(IColor.New<RGB>(r1 * 255, g1 * 255, b1 * 255), profile);

                hsb2 = new HSB();
                hsb2.From(IColor.New<RGB>(r2 * 255, g2 * 255, b2 * 255), profile);

                IColor.New<HSB>(hsb2.X, hsb1.Y, hsb1.Z).To(out rgb, profile);
                x = rgb.X; y = rgb.Y; z = rgb.Z;
                break;
            case BlendModes.ColorBurn:
                //R = 1 - (1 - a) / b
                x = Math.Clamp(1 - (1 - r1) / r2, 0, 1);
                y = Math.Clamp(1 - (1 - g1) / g2, 0, 1);
                z = Math.Clamp(1 - (1 - b1) / b2, 0, 1);
                break;
            case BlendModes.ColorDodge:
                //R = a / (1 - b)
                x = BlendColorDodgef(r1, r2);
                y = BlendColorDodgef(g1, g2);
                z = BlendColorDodgef(b1, b2);
                break;
            case BlendModes.Darken:
                x = r1 < r2 ? r1 : r2;
                y = g1 < g2 ? g1 : g2;
                z = b1 < b2 ? b1 : b2;
                break;
            case BlendModes.Difference:
                x = (r1 - r2).Abs();
                y = (g1 - g2).Abs();
                z = (b1 - b2).Abs();
                break;
            case BlendModes.Exclusion:
                x = r1 + r2 - (2 * r1 * r2);
                y = g1 + g2 - (2 * g1 * g2);
                z = b1 + b2 - (2 * b1 * b2);
                break;
            case BlendModes.Glow:
                x = BlendReflectf(r2, r1);
                y = BlendReflectf(g2, g1);
                z = BlendReflectf(b2, b1);
                break;
            case BlendModes.HardLight:
                x = r1 < 0.5 ? r1 * r2 : (r1 <= 1 || r2 <= 1 ? r1 + r2 - (r1 * r2) : Math.Max(r1, r2));
                y = g1 < 0.5 ? g1 * g2 : (g1 <= 1 || g2 <= 1 ? g1 + g2 - (g1 * g2) : Math.Max(g1, g2));
                z = b1 < 0.5 ? b1 * b2 : (b1 <= 1 || b2 <= 1 ? b1 + b2 - (b1 * b2) : Math.Max(b1, b2));
                break;
            case BlendModes.HardMix:
                x = BlendHardMixf(r1, r2);
                y = BlendHardMixf(g1, g2);
                z = BlendHardMixf(b1, b2);
                break;
            case BlendModes.Hue:
                hsb1 = new HSB();
                hsb1.From(IColor.New<RGB>(r1 * 255, g1 * 255, b1 * 255), profile);

                hsb2 = new HSB();
                hsb2.From(IColor.New<RGB>(r2 * 255, g2 * 255, b2 * 255), profile);

                IColor.New<HSB>(hsb2.X, hsb1.Y, hsb1.Z).To(out rgb, profile);
                x = rgb.X; y = rgb.Y; z = rgb.Z;
                break;
            case BlendModes.Lighten:
                x = r1 > r2 ? r1 : r2;
                y = g1 > g2 ? g1 : g2;
                z = b1 > b2 ? b1 : b2;
                break;
            case BlendModes.LinearBurn:
                //R = a + b - 1
                x = Math.Clamp(r1 + r2 - 1, 0, 1);
                y = Math.Clamp(g1 + g2 - 1, 0, 1);
                z = Math.Clamp(b1 + b2 - 1, 0, 1);
                break;
            case BlendModes.LinearDodge:
                x = r1 + r2;
                y = g1 + g2;
                z = b1 + b2;
                break;
            case BlendModes.LinearLight:
                //if (Blend > ½) R = Base + 2×(Blend-½); if (Blend <= ½) R = Base + 2×Blend - 1
                x = r2 > 0.5 ? r1 + 2 * (r2 - 0.5) : r1 + 2 * r2 - 1;
                y = g2 > 0.5 ? g1 + 2 * (g2 - 0.5) : g1 + 2 * g2 - 1;
                z = b2 > 0.5 ? b1 + 2 * (b2 - 0.5) : b1 + 2 * b2 - 1;
                break;
            case BlendModes.Luminosity:
                hsb1 = new HSB();
                hsb1.From(IColor.New<RGB>(r1 * 255, g1 * 255, b1 * 255), profile);

                hsb2 = new HSB();
                hsb2.From(IColor.New<RGB>(r2 * 255, g2 * 255, b2 * 255), profile);

                IColor.New<HSB>(hsb2.X, hsb1.Y, hsb1.Z).To(out rgb, profile);
                x = rgb.X; y = rgb.Y; z = rgb.Z;
                break;
            case BlendModes.Multiply:
                x = r1 * r2;
                y = g1 * g2;
                z = b1 * b2;
                break;
            case BlendModes.Negation:
                x = 1 - Math.Abs(1 - r1 - r2);
                y = 1 - Math.Abs(1 - g1 - g2);
                z = 1 - Math.Abs(1 - b1 - b2);
                break;
            case BlendModes.Normal:
                w = 1.0 - (1.0 - a2) * (1.0 - a1);
                x = r2 * a2 / w + r1 * a1 * (1.0 - a2) / w;
                y = g2 * a2 / w + g1 * a1 * (1.0 - a2) / w;
                z = b2 * a2 / w + b1 * a1 * (1.0 - a2) / w;

                w = double.IsNaN(w) ? 0 : w;
                x = double.IsNaN(x) ? 0 : x;
                y = double.IsNaN(y) ? 0 : y;
                z = double.IsNaN(z) ? 0 : z;
                break;
            case BlendModes.Overlay:
                //if (Base > ½) R = 1 - (1-2×(Base-½)) × (1-Blend); if (Base <= ½) R = (2×Base) × Blend
                x = r1 > 0.5 ? 1 - (1 - 2 * (r1 - 0.5)) * (1 - r2) : (2 * r1) * r2;
                y = g1 > 0.5 ? 1 - (1 - 2 * (g1 - 0.5)) * (1 - g2) : (2 * g1) * g2;
                z = b1 > 0.5 ? 1 - (1 - 2 * (b1 - 0.5)) * (1 - b2) : (2 * b1) * b2;
                break;
            case BlendModes.Phoenix:
                x = r1 - r2;
                y = g1 - g2;
                z = b1 - b2;
                break;
            case BlendModes.PinLight:
                //if (Blend > ½) R = max(Base,2×(Blend-½)); if (Blend <= ½) R = min(Base,2×Blend))
                x = r2 > 0.5 ? Math.Max(r1, 2 * (r2 - 0.5)) : Math.Min(r1, 2 * r2);
                y = g2 > 0.5 ? Math.Max(g1, 2 * (g2 - 0.5)) : Math.Min(g1, 2 * g2);
                z = b2 > 0.5 ? Math.Max(b1, 2 * (b2 - 0.5)) : Math.Min(b1, 2 * b2);
                break;
            case BlendModes.Reflect:
                x = r1 / (r2 == 0 ? 0.01 : r2);
                y = g1 / (g2 == 0 ? 0.01 : g2);
                z = b1 / (b2 == 0 ? 0.01 : b2);
                break;
            case BlendModes.Saturation:
                hsb1 = new HSB();
                hsb1.From(IColor.New<RGB>(r1 * 255, g1 * 255, b1 * 255), profile);

                hsb2 = new HSB();
                hsb2.From(IColor.New<RGB>(r2 * 255, g2 * 255, b2 * 255), profile);

                IColor.New<HSB>(hsb2.X, hsb1.Y, hsb1.Z).To(out rgb, profile);
                x = rgb.X; y = rgb.Y; z = rgb.Z;
                break;
            case BlendModes.Screen:
                x = r1 <= 1 || r2 <= 1 ? r1 + r2 - (r1 * r2) : Math.Max(r1, r2);
                y = g1 <= 1 || g2 <= 1 ? g1 + g2 - (g1 * g2) : Math.Max(g1, g2);
                z = b1 <= 1 || b2 <= 1 ? b1 + b2 - (b1 * b2) : Math.Max(b1, b2);
                break;
            case BlendModes.SoftLight:
                x = BlendSoftLightf(r1, r2);
                y = BlendSoftLightf(g1, g2);
                z = BlendSoftLightf(b1, b2);
                break;
            case BlendModes.VividLight:
                x = BlendVividLightf(r1, r2);
                y = BlendVividLightf(g1, g2);
                z = BlendVividLightf(b1, b2);
                break;
        }

        return new ByteVector4(x.Denormalize<byte>(), y.Denormalize<byte>(), z.Denormalize<byte>(), w.Denormalize<byte>(), a.Type);
    }

    #endregion

    #region Convert

    /// <summary><see cref="RGB"/> ⇒ <see cref="ByteVector4"/></summary>
    public static ByteVector4 Convert(RGB i) => new(i.XYZ);

    /// <summary><see cref="ByteVector4"/> ⇒ <see cref="RGB"/></summary>
    public static RGB Convert(IVector3<byte> i) => new(i.X, i.Y, i.Z);

    #endregion

    #region GetName

    private const BindingFlags ColorNameFlags = BindingFlags.Public | BindingFlags.Static;

    private static readonly Type[] ColorNameTypes =
    [
        typeof(System.Drawing.Color),

        typeof(Colors1), typeof(Colors2), typeof(Colors3), typeof(Colors4), typeof(Colors5),

        typeof(ColorPreset.CSS),
        typeof(ColorPreset.WebBasic),  typeof(ColorPreset.WebSafe), typeof(ColorPreset.WebSafest),
    ];

    private static Dictionary<ByteVector3, string> colors;
    private static Dictionary<ByteVector3, string> Colors
    {
        get
        {
            if (colors is null)
            {
                colors = new();
                foreach (var i in ColorNameTypes)
                {
                    if (i == typeof(System.Drawing.Color))
                    {
                        var properties = typeof(System.Drawing.Color).GetProperties(ColorNameFlags).Where(p => p.PropertyType == typeof(System.Drawing.Color));
                        foreach (var property in properties)
                        {
                            var value = (System.Drawing.Color)property.GetValue(null, null);
                            colors.Add(new ByteVector3(value.R, value.G, value.B), property.Name);
                        }
                    }
                    else
                    {
                        var fields = i.GetFields(ColorNameFlags);
                        foreach (var @field in fields)
                            colors.Add(new ByteVector3((string)@field.GetValue(null)), @field.Name);

                    }
                }
            }
            return colors;
        }
    }

    public const string ColorNameFormat = "{0}";

    public const string ColorNameFormatApproximate = "~ {0}";

    public static string GetName(this ByteVector2 color) => new ByteVector3(color).GetName();

    public static string GetName(this ByteVector3 color) => new ByteVector4(color).GetName();

    public static string GetName(this ByteVector4 color)
    {
        int mDistance
            = int.MaxValue;
        string mColor
            = System.Drawing.Color.Black.Name;

        foreach (var i in Colors)
        {
            if (i.Key == color.XYZ)
                return ColorNameFormat.F(i.Value.GetCamel());

            int distance = Math.Abs(i.Key.R - color.R) + Math.Abs(i.Key.G - color.G) + Math.Abs(i.Key.B - color.B);
            if (distance < mDistance)
            {
                mDistance = distance;
                mColor = i.Value;
            }
        }

        return ColorNameFormatApproximate.F(mColor.GetCamel());
    }

    #endregion

    #region Decode

    [NotTested]
    public static ByteVector4 Decode(this int color)
    {
        var alpha = (byte)(color >> 24);

        /// Prevent division by zero
        int ai = alpha == 0 ? 1 : alpha;

        /// Scale inverse alpha to use cheap integer mul bit shift
        ai = (255 << 8) / ai;

        return new ByteVector4
        (
            (byte)((((color >> 16) & 0xFF) * ai) >> 8),
            (byte)((((color >> 8) & 0xFF) * ai) >> 8),
            (byte)(((color & 0xFF) * ai) >> 8),
            alpha
        );
    }

    #endregion

    #region Encode

    [NotTested]
    public static int Encode(this IVector3<byte> input, ColorFormat format = ColorFormat.Default)
        => new ByteVector4(input.X, input.Y, input.Z, byte.MaxValue).Encode(format);

    [NotTested]
    public static int Encode(this IVector4<byte> input, ColorFormat format = ColorFormat.Default)
    {
        var result = 0;
        if (format == ColorFormat.BlackWhite)
        {

        }
        else if (format == ColorFormat.Bgr101010)
        {
            result = input.X << 20;
            result |= input.Y << 10;
            result |= input.Z << 0;
        }
        else if (format == ColorFormat.Bgr24)
        {
            result = input.X << 16;
            result |= input.Y << 8;
            result |= input.Z << 0;
        }
        else if (format == ColorFormat.Bgr32)
        {
            result = 255 << 24;
            result |= input.X << 16;
            result |= input.Y << 8;
            result |= input.Z << 0;
        }
        else if (format == ColorFormat.Bgr555)
        {
            result = input.X << 10;
            result |= input.Y << 5;
            result |= input.Z << 0;
        }
        else if (format == ColorFormat.Bgr565)
        {
            result = (input.X >> 3) << 11;
            result |= (input.Y >> 2) << 5;
            result |= input.Z >> 3;
        }
        else if (format == ColorFormat.Bgra32)
        {
            result = input.W << 24;
            result |= input.X << 16;
            result |= input.Y << 8;
            result |= input.Z << 0;
        }
        else if (format == ColorFormat.Cmyk32)
        {
            byte c = 0, m = 0, y = 0, k = 0;
            result = c << 24;
            result |= m << 16;
            result |= y << 8;
            result |= k;
        }
        else if (format == ColorFormat.Default)
        {
            result = 0;
            if (input.W != 0)
            {
                var a = input.W + 1;
                result = (input.W << 24)
                | ((byte)((input.X * a) >> 8) << 16)
                | ((byte)((input.Y * a) >> 8) << 8)
                | ((byte)((input.Z * a) >> 8));
            }
        }
        else if (format == ColorFormat.Gray2)
        {
            /// Based on ITU-R BT.601 standard for luminance calculation
            int gray = (input.X * 30 + input.Y * 59 + input.Z * 11) / 100;
            /// Move 6 bits to left, clearing top 6. Then move back to original 6-bit range.
            result = (gray << 6) >> 6;
        }
        else if (format == ColorFormat.Gray4)
        {
            /// Based on ITU-R BT.601 standard for luminance calculation
            int gray = (input.X * 30 + input.Y * 59 + input.Z * 11) / 100;
            /// Move 12 bits to left, clearing top 12. Then move back to original 4-bit range.
            result = (gray << 12) >> 12;
        }
        else if (format == ColorFormat.Gray8)
        {
            /// A simplified version of the ITU-R 601-2 luminance calculation
            int gray = (input.X * 30 + input.Y * 59 + input.Z * 11) / 100;
            /// Moves bits 24 positions to left, clearing top 24 bits. Then bring bits back to original 8-bit range.
            result = (gray << 24) >> 24;
        }
        else if (format == ColorFormat.Gray16)
        {
            /// Based on ITU-R BT.601 standard for luminance calculation
            int gray = (input.X * 30 + input.Y * 59 + input.Z * 11) / 100;
            /// Moves bits 48 positions to left, clearing top 48 bits. Then bring bits back to original 16-bit range.
            result = (gray << 48) >> 48;
        }
        else if (format == ColorFormat.Gray32Float)
        {
            /// Based on ITU-R BT.601 standard for luminance calculation
            int gray = (input.X * 30 + input.Y * 59 + input.Z * 11) / 100;
            /// Moves bits 96 positions to left, clearing top 96 bits. Then bring bits back to original 32-bit range.
            result = (gray << 96) >> 96;
        }
        else if (format == ColorFormat.Indexed1)
        {

        }
        else if (format == ColorFormat.Indexed2)
        {

        }
        else if (format == ColorFormat.Indexed4)
        {

        }
        else if (format == ColorFormat.Indexed8)
        {

        }
        else if (format == ColorFormat.Rgb24)
        {
            result = input.Z << 16;
            result |= input.Y << 8;
            result |= input.X << 0;
        }
        else if (format == ColorFormat.Rgb48)
        {
            result = input.Z << 32;
            result |= input.Y << 16;
            result |= input.X << 0;
        }
        else if (format == ColorFormat.Rgb128Float)
        {
            result = 255 << 96;
            result |= input.Z << 64;
            result |= input.Y << 32;
            result |= input.X << 0;
        }
        else if (format == ColorFormat.Rgba64)
        {
            result = input.W << 48;
            result |= input.Z << 32;
            result |= input.Y << 16;
            result |= input.X << 0;
        }
        else if (format == ColorFormat.Rgba128Float)
        {
            result = input.W << 96;
            result |= input.Z << 64;
            result |= input.Y << 32;
            result |= input.X << 0;
        }
        else if (format == ColorFormat.Pbgra32)
        {
            result = (input.W << 24)
                    | (input.X << 16)
                    | (input.Y << 8)
                    | input.Z;
        }
        else if (format == ColorFormat.Prgba64)
        {
            result = (input.W << 48)
                    | (input.X << 32)
                    | (input.Y << 16)
                    | input.Z;
        }
        else if (format == ColorFormat.Prgba128Float)
        {
            result = (input.W << 96)
                    | (input.X << 64)
                    | (input.Y << 32)
                    | input.Z;
        }
        return result;
    }

    #endregion
}