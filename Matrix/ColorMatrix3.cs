using Ion.Numeral;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Colors;

/// <summary>
/// An <see cref="IMatrix"/> of <see cref="ByteVector3"/>.
/// </summary>
[Description(IMatrix.Description)]
public readonly record struct ColorMatrix3
    : IMatrix<ColorMatrix3, ByteVector3>, IMatrix2D<ByteVector3>, IMatrixUnfixedAlias<ColorMatrix3, ByteVector3>, IMatrixImmutable, IMatrixUnfixed<ByteVector3>
{
    public static ByteVector3 DefaultColor { get; } = ByteVector3.Black;

    /// <see cref="Region.Field"/>

    private readonly ByteVector3[][] _Value;

    /// <see cref="Region.Property"/>

    public int Columns => _Value[0].Length;

    public int Rows => _Value.Length;

    /// <see cref="Region.Property.Indexor"/>

    object IArray.this[int y] => this[y];

    object IArray1D.this[int y] => this[y];

    object IArray2D.this[int y, int x] => this[y, x];

#pragma warning disable CA1819 /// Properties should not return arrays
    public ByteVector3[] this[int row] { get => _Value[row]; set => _Value[row] = value; }
#pragma warning restore CA1819

    public ByteVector3 this[int row, int column] { get => _Value[row][column]; set => _Value[row][column] = value; }

    /// <see cref="Region.Constructor"/>

    /// <inheritdoc cref="IMatrix.Format{T}(int, in T)"/>
    public ColorMatrix3(int length, in IVector3<byte> color = default)
        => IMatrix.Format(length, color);

    /// <inheritdoc cref="IMatrix.Format{T}(int, int, in T)"/>
    public ColorMatrix3(int rows, int columns, in IVector3<byte> color = default)
    {
        Throw.IfNull(color, nameof(color));
        _Value = Array2D.Get(rows, columns, new ByteVector3(color));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in T[], Matrix2DFromArray1D)"/>
    public ColorMatrix3(in ByteVector3[] colors, Matrix2DFromArray1D fill = default)
        => IMatrix.Format(colors, fill);

    /// <inheritdoc cref="IMatrix.Format{T}(in T[], Matrix2DFromArray1D)"/>
    public ColorMatrix3(in ByteVector4[] colors, Matrix2DFromArray1D fill = default)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.ToArray(i => new ByteVector3(i)), fill);
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in T[][])"/>
    public ColorMatrix3(in ByteVector3[][] colors)
        => _Value = IMatrix.Format(colors);

    /// <inheritdoc cref="IMatrix.Format{T}(in T[][])"/>
    public ColorMatrix3(in ByteVector4[][] colors)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.ToArray(i => new ByteVector3(i)));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in T[][][])"/>
    public ColorMatrix3(in ByteVector3[][][] colors)
        => _Value = IMatrix.Format(colors);

    /// <inheritdoc cref="IMatrix.Format{T}(in T[][][])"/>
    public ColorMatrix3(in ByteVector4[][][] colors)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.ToArray(i => new ByteVector3(i)));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray1D{T}, Matrix2DFromArray1D)"/>
    public ColorMatrix3(in IArray1D<ByteVector3> colors, Matrix2DFromArray1D fill = default)
        => _Value = IMatrix.Format(colors, fill);

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray1D{T}, Matrix2DFromArray1D)"/>
    public ColorMatrix3(in IArray1D<ByteVector4> colors, Matrix2DFromArray1D fill = default)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.ToArray().ToArray(i => new ByteVector3(i)), fill);
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray2D{T})"/>
    public ColorMatrix3(in IArray2D<ByteVector3> colors)
        => _Value = IMatrix.Format(colors);

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray2D{T})"/>
    public ColorMatrix3(in IArray2D<ByteVector4> colors)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.ToArray().ToArray(i => new ByteVector3(i)));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray3D{T})"/>
    public ColorMatrix3(in IArray3D<ByteVector3> colors)
        => _Value = IMatrix.Format(colors);

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray3D{T})"/>
    public ColorMatrix3(in IArray3D<ByteVector4> colors)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.ToArray().ToArray(i => new ByteVector3(i)));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in IMatrix2D{T})"/>
    public ColorMatrix3(in IMatrix2D<ByteVector3> colors)
        => _Value = IMatrix.Format(colors);

    /// <inheritdoc cref="IMatrix.Format{T}(in IMatrix2D{T})"/>
    public ColorMatrix3(in IMatrix2D<ByteVector4> colors)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.NewType(i => new ByteVector3(i)));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in IMatrix3D{T})"/>
    public ColorMatrix3(in IMatrix3D<ByteVector3> colors)
        => _Value = IMatrix.Format(colors);

    /// <inheritdoc cref="IMatrix.Format{T}(in IMatrix3D{T})"/>
    public ColorMatrix3(in IMatrix3D<ByteVector4> colors)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.NewType(i => new ByteVector3(i)));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in IVector{T}, int)"/>
    public ColorMatrix3(in IVector<ByteVector3> colors, int repeat = 1)
        => _Value = IMatrix.Format(colors, repeat);

    /// <inheritdoc cref="IMatrix.Format{T}(in IVector{T}, int)"/>
    public ColorMatrix3(in IVector<ByteVector4> colors, int repeat = 1)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.NewType(i => new ByteVector3(i)), repeat);
    }

    /// <see cref="Region.Operator"/>
    #region

    public static ColorMatrix3 operator +(ColorMatrix3 a, IVector3<byte> b) => a.New(i => i + b);

    public static ColorMatrix3 operator +(ColorMatrix3 a, IMatrix2D<ByteVector3> b) => a.New((y, x, i) => i + b[y, x]);

    public static ColorMatrix3 operator +(ColorMatrix3 a, IMatrix2D<ByteVector4> b) => a.New((y, x, i) => i + new ByteVector3(b[y, x]));

    public static ColorMatrix3 operator ++(ColorMatrix3 a) => a.New((y, x, i) => i++);

    public static ColorMatrix3 operator -(ColorMatrix3 a) => a.New(i => -i);

    public static ColorMatrix3 operator -(ColorMatrix3 a, IVector3<byte> b) => a.New(i => i - b);

    public static ColorMatrix3 operator -(ColorMatrix3 a, IMatrix2D<ByteVector3> b) => a.New((y, x, i) => i - b[y, x]);

    public static ColorMatrix3 operator -(ColorMatrix3 a, IMatrix2D<ByteVector4> b) => a.New((y, x, i) => i - new ByteVector3(b[y, x]));

    public static ColorMatrix3 operator --(ColorMatrix3 a) => a.New((y, x, i) => i--);

    public static ColorMatrix3 operator *(ColorMatrix3 a, IVector3<byte> b) => a.New(i => i * b);

    public static ColorMatrix3 operator *(ColorMatrix3 a, IMatrix2D<ByteVector3> b) => a.New((y, x, i) => i * b[y, x]);

    public static ColorMatrix3 operator *(ColorMatrix3 a, IMatrix2D<ByteVector4> b) => a.New((y, x, i) => i * new ByteVector3(b[y, x]));

    public static ColorMatrix3 operator /(ColorMatrix3 a, IVector3<byte> b) => a.New(i => i / b);

    public static ColorMatrix3 operator /(ColorMatrix3 a, IMatrix2D<ByteVector3> b) => a.New((y, x, i) => i / b[y, x]);

    public static ColorMatrix3 operator /(ColorMatrix3 a, IMatrix2D<ByteVector4> b) => a.New((y, x, i) => i / new ByteVector3(b[y, x]));

    public static ColorMatrix3 operator %(ColorMatrix3 a, IVector3<byte> b) => a.New(i => i % b);

    public static ColorMatrix3 operator %(ColorMatrix3 a, IMatrix2D<ByteVector3> b) => a.New((y, x, i) => i % b[y, x]);

    public static ColorMatrix3 operator %(ColorMatrix3 a, IMatrix2D<ByteVector4> b) => a.New((y, x, i) => i % new ByteVector3(b[y, x]));

    #endregion

    /// <see cref="IArray2D"/>

    object[][] IArray2D.ToArray() => XArray2D.ToArray(_Value, i => (object)i);

    public ByteVector3[][] ToArray() => [.. _Value];

    public ByteVector3[] ToArray(int y) => [.. _Value[y]];

    /// <see cref="IArray{,}"/>

    Array IArray<ColorMatrix3, ByteVector3>.GetArray() => new ByteVector3[Rows][];

    static ColorMatrix3 IArray<ColorMatrix3, ByteVector3>.Create(ColorMatrix3 oldSelf, Array newSelf) => new(newSelf as ByteVector3[][]);

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<ByteVector3>).GetEnumerator();

    readonly IEnumerator<ByteVector3> IEnumerable<ByteVector3>.GetEnumerator() { foreach (ByteVector3[] y in _Value) { foreach (ByteVector3 x in y) { yield return x; } } }

    /// <see cref="IFormattable"/>

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public readonly string ToString(string format, IFormatProvider provider) => IMatrix.ToString(this, format, provider);
}