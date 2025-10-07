using Ion.Numeral;
using Ion.Reflect;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Colors;

/// <inheritdoc cref="IColor"/>
[Description(IColor.Description)]
public abstract record class Color<TSelf, TNumber>()
    : object(), IColor<TNumber>, IConvert<Lrgb>, IConvert<RGB>
    where TNumber : System.Numerics.INumber<TNumber>
    where TSelf : IColor<TSelf, TNumber>, System.Numerics.IMinMaxValue<TSelf>
{
    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="IVector.Length"/>
    public abstract int Length { get; }

    /// <see cref="Region.Operator"/>

    public static implicit operator Vector<TNumber>(in Color<TSelf, TNumber> input) => new(input.ToArray());

    /// <see cref="Region.Method"/>

    /// <summary>(🗸) <see cref="LMS"/> (0) > <see cref="LMS"/> (1)</summary>
    protected static LMS Adapt(LMS color, LMS sW, LMS dW)
    {
        var result = new Matrix3x3<double>(dW.X / sW.X, dW.Y / sW.Y, dW.Z / sW.Z, Matrix3x3Fill.DiagonalRight) * color.XYZ;
        return IColor.New<LMS>(result);
    }

    /// <summary>(🗸) <see cref="LMS"/> (0) > <see cref="LMS"/> (1)</summary>
    protected static LMS Adapt(LMS input, ColorProfile source, ColorProfile target)
    {
        //XYZ (0) > LMS (0)
        var a = new LMS();
        a.From((XYZ)(xyY)(XY)source.Chromacity, source);

        //XYZ (1) > LMS (1)
        var b = new LMS();
        b.From((XYZ)(xyY)(XY)target.Chromacity, source);

        //LMS (0) > LMS (1)
        return Adapt(input, a, b);
    }

    /// <summary>(🗸) <see cref="RGB"/> (0) > <see cref="XYZ"/> (0) > <see cref="LMS"/> (0) > <see cref="LMS"/> (1) > <see cref="XYZ"/> (1) > <see cref="RGB"/> (1)</summary>
    protected static RGB Adapt(RGB input, ColorProfile source, ColorProfile target)
    {
        //RGB (0) > XYZ (0)
        var xyz = new XYZ();
        xyz.From(input, source);

        //XYZ (0) > LMS (0) > LMS (1) > XYZ (1)
        xyz = Adapt(xyz, source, target);

        //XYZ (1) > RGB (1)
        xyz.To(out RGB result, target);
        return result;
    }

    /// <summary>(🗸) <see cref="XYZ"/> (0) > <see cref="LMS"/> (0) > <see cref="LMS"/> (1) > <see cref="XYZ"/> (1)</summary>
    protected static XYZ Adapt(XYZ input, ColorProfile source, ColorProfile target)
    {
        //XYZ (0) > LMS (0)
        var lms = new LMS();
        lms.From(input, source);

        //LMS (0) > LMS (1)
        lms = Adapt(lms, source, target);

        //LMS (1) > XYZ (1)
        lms.To(out XYZ result, target);
        return result;
    }

    /// <summary>(🗸) <see cref="IColor">this</see> (0) > <see cref="RGB"/> (0) > <see cref="XYZ"/> (0) > <see cref="LMS"/> (0) > <see cref="LMS"/> (1) > <see cref="XYZ"/> (1) > <see cref="RGB"/> (1) > <see cref="IColor">this</see> (1)</summary>
    public virtual void Adapt(ColorProfile source, ColorProfile target)
    {
        To(out RGB result, source);

        var final = Adapt(result, source, target);
        From(final, target);
    }

    ///

    /// <summary><see cref="Lrgb"/> ⇒ <see langword="this"/></summary>
    public abstract void From(in Lrgb input, ColorProfile profile);

    /// <summary><see cref="RGB"/> ⇒ <see langword="this"/></summary>
    public virtual void From(in RGB input, ColorProfile profile)
    {
        var result = input.To(profile);
        From(result, profile);
    }

    /// <summary><see langword="this"/> ⇒ <see cref="Lrgb"/></summary>
    public void To(out Lrgb result, ColorProfile profile) => result = To(profile);

    /// <summary><see langword="this"/> ⇒ <see cref="RGB"/></summary>
    public virtual void To(out RGB result, ColorProfile profile)
    {
        var a = To(profile);

        var b = new RGB();
        b.From(a, profile);
        result = b;
    }

    /// <summary><see langword="this"/> ⇒ <see cref="IColor"/></summary>
    public T To<T>(ColorProfile profile = default) where T : IColor, new() => (T)To(typeof(T), profile);

    /// <summary><see langword="this"/> ⇒ <see cref="IColor"/></summary>
    public IColor To(Type model, ColorProfile profile = default)
    {
        //this > RGB
        To(out RGB rgb, profile);

        //RGB > T
        var result = model.Create<IColor>();
        result.From(rgb, profile);
        return result;
    }

    ///

    /// <summary>this ⇒ <see cref="Lrgb"/></summary>
    public abstract Lrgb To(ColorProfile profile);

    ///

    public abstract TNumber[] ToArray();

    /// <see cref="IArray"/>

    object IArray.this[int i] => ToArray()[i];

    /// <see cref="IArray1D{}"/>

    TNumber IArray1D<TNumber>.this[int i] => ToArray()[i];

    /// <see cref="IColor"/>

    double[] IColor.ToArray() => ToArray().ToArray(i => i.ToDouble());

    /// <see cref="IEnumerable"/>

    IEnumerator IEnumerable.GetEnumerator() => ToArray().GetEnumerator();

    /// <see cref="IEnumerable{TNumber}"/>

    IEnumerator<TNumber> IEnumerable<TNumber>.GetEnumerator() => ((IEnumerable<TNumber>)ToArray()).GetEnumerator();

    /// <see cref="IFormattable"/>

    /// <inheritdoc/>
    public sealed override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc/>
    public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc/>
    public virtual string ToString(string format, IFormatProvider provider) => IVector.ToString(this, format, provider);

    /// <see cref="IVector"/>

    /// <inheritdoc cref="IVector.Type"/>
    public VectorType Type { get; set; }
}