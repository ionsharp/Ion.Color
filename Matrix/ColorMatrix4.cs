using Ion.Numeral;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Colors;

/// <summary>
/// An <see cref="IMatrix"/> of <see cref="ByteVector4"/> (with alpha channel).
/// </summary>
[Description(IMatrix.Description)]
public readonly record struct ColorMatrix4
    : IMatrix<ColorMatrix4, ByteVector4>, IMatrix2D<ByteVector4>, IMatrixUnfixedAlias<ColorMatrix4, ByteVector4>, IMatrixImmutable, IMatrixUnfixed<ByteVector4>
{
    public static ByteVector4 DefaultColor { get; } = ByteVector4.Transparent;

    /// <see cref="Region.Field"/>

    private readonly ByteVector4[][] _Value;

    /// <see cref="Region.Property"/>

    public int Columns => _Value[0].Length;

    public int Rows => _Value.Length;

    /// <see cref="Region.Property.Indexor"/>

    object IArray.this[int y] => this[y];

    object IArray1D.this[int y] => this[y];

    object IArray2D.this[int y, int x] => this[y, x];

#pragma warning disable CA1819 /// Properties should not return arrays
    public ByteVector4[] this[int row] { get => _Value[row]; set => _Value[row] = value; }
#pragma warning restore CA1819

    public ByteVector4 this[int row, int column] { get => _Value[row][column]; set => _Value[row][column] = value; }

    /// <see cref="Region.Constructor"/>

    /// <inheritdoc cref="IMatrix.Format{T}(int, in T)"/>
    public ColorMatrix4(int length, in IVector3<byte> color)
        => IMatrix.Format(length, color);

    /// <inheritdoc cref="IMatrix.Format{T}(int, in T)"/>
    public ColorMatrix4(int length, in IVector4<byte> color = default)
        => IMatrix.Format(length, color);

    /// <inheritdoc cref="IMatrix.Format{T}(int, int, in T)"/>
    public ColorMatrix4(int rows, int columns, in IVector3<byte> color)
    {
        Throw.IfNull(color, nameof(color));
        _Value = IMatrix.Format(rows, columns, new ByteVector4(color));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(int, int, in T)"/>
    public ColorMatrix4(int rows, int columns, in IVector4<byte> color = default)
    {
        var _color = color is null ? ByteVector4.Transparent : new ByteVector4(color);
        _Value = Array2D.Get(rows, columns, _color);
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in T[], Matrix2DFromArray1D)"/>
    public ColorMatrix4(in ByteVector3[] colors, Matrix2DFromArray1D fill = default)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.ToArray(i => new ByteVector4(i)), fill);
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in T[], Matrix2DFromArray1D)"/>
    public ColorMatrix4(in ByteVector4[] colors, Matrix2DFromArray1D fill = default)
        => IMatrix.Format(colors, fill);

    /// <inheritdoc cref="IMatrix.Format{T}(in T[][])"/>
    public ColorMatrix4(in ByteVector3[][] colors)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.ToArray(i => new ByteVector4(i)));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in T[][])"/>
    public ColorMatrix4(in ByteVector4[][] colors)
        => _Value = IMatrix.Format(colors);

    /// <inheritdoc cref="IMatrix.Format{T}(in T[][][])"/>
    public ColorMatrix4(in ByteVector3[][][] colors)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.ToArray(i => new ByteVector4(i)));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in T[][][])"/>
    public ColorMatrix4(in ByteVector4[][][] colors)
        => _Value = IMatrix.Format(colors);

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray1D{T}, Matrix2DFromArray1D)"/>
    public ColorMatrix4(in IArray1D<ByteVector3> colors, Matrix2DFromArray1D fill = default)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.ToArray().ToArray(i => new ByteVector4(i)), fill);
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray1D{T}, Matrix2DFromArray1D)"/>
    public ColorMatrix4(in IArray1D<ByteVector4> colors, Matrix2DFromArray1D fill = default)
        => _Value = IMatrix.Format(colors, fill);

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray2D{T})"/>
    public ColorMatrix4(in IArray2D<ByteVector3> colors)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.ToArray().ToArray(i => new ByteVector4(i)));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray2D{T})"/>
    public ColorMatrix4(in IArray2D<ByteVector4> colors)
        => _Value = IMatrix.Format(colors);

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray3D{T})"/>
    public ColorMatrix4(in IArray3D<ByteVector3> colors)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.ToArray().ToArray(i => new ByteVector4(i)));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray3D{T})"/>
    public ColorMatrix4(in IArray3D<ByteVector4> colors)
        => _Value = IMatrix.Format(colors);

    /// <inheritdoc cref="IMatrix.Format{T}(in IMatrix2D{T})"/>
    public ColorMatrix4(in IMatrix2D<ByteVector3> colors)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.NewType(i => new ByteVector4(i)));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in IMatrix2D{T})"/>
    public ColorMatrix4(in IMatrix2D<ByteVector4> colors)
        => _Value = IMatrix.Format(colors);

    /// <inheritdoc cref="IMatrix.Format{T}(in IMatrix3D{T})"/>
    public ColorMatrix4(in IMatrix3D<ByteVector3> colors)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.NewType(i => new ByteVector4(i)));
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in IMatrix3D{T})"/>
    public ColorMatrix4(in IMatrix3D<ByteVector4> colors)
        => _Value = IMatrix.Format(colors);

    /// <inheritdoc cref="IMatrix.Format{T}(in IVector{T}, int)"/>
    public ColorMatrix4(in IVector<ByteVector3> colors, int repeat = 1)
    {
        Throw.IfNull(colors, nameof(colors));
        _Value = IMatrix.Format(colors.NewType(i => new ByteVector4(i)), repeat);
    }

    /// <inheritdoc cref="IMatrix.Format{T}(in IVector{T}, int)"/>
    public ColorMatrix4(in IVector<ByteVector4> colors, int repeat = 1)
        => _Value = IMatrix.Format(colors, repeat);

    /// <see cref="Region.Operator"/>
    #region

    public static ColorMatrix4 operator +(ColorMatrix4 a, IVector3<byte> b) => a.New(i => i + new ByteVector4(b));

    public static ColorMatrix4 operator +(ColorMatrix4 a, IMatrix2D<ByteVector3> b) => a.New((y, x, i) => i + new ByteVector4(b[y, x]));

    public static ColorMatrix4 operator +(ColorMatrix4 a, IMatrix2D<ByteVector4> b) => a.New((y, x, i) => i + b[y, x]);

    public static ColorMatrix4 operator ++(ColorMatrix4 a) => a.New((y, x, i) => i++);

    public static ColorMatrix4 operator -(ColorMatrix4 a) => a.New(i => -i);

    public static ColorMatrix4 operator -(ColorMatrix4 a, IVector3<byte> b) => a.New(i => i - new ByteVector4(b));

    public static ColorMatrix4 operator -(ColorMatrix4 a, IMatrix2D<ByteVector3> b) => a.New((y, x, i) => i - new ByteVector4(b[y, x]));

    public static ColorMatrix4 operator -(ColorMatrix4 a, IMatrix2D<ByteVector4> b) => a.New((y, x, i) => i - b[y, x]);

    public static ColorMatrix4 operator --(ColorMatrix4 a) => a.New((y, x, i) => i--);

    public static ColorMatrix4 operator *(ColorMatrix4 a, IVector3<byte> b) => a.New(i => i * new ByteVector4(b));

    public static ColorMatrix4 operator *(ColorMatrix4 a, IMatrix2D<ByteVector3> b) => a.New((y, x, i) => i * new ByteVector4(b[y, x]));

    public static ColorMatrix4 operator *(ColorMatrix4 a, IMatrix2D<ByteVector4> b) => a.New((y, x, i) => i * b[y, x]);

    public static ColorMatrix4 operator /(ColorMatrix4 a, IVector3<byte> b) => a.New(i => i / new ByteVector4(b));

    public static ColorMatrix4 operator /(ColorMatrix4 a, IMatrix2D<ByteVector3> b) => a.New((y, x, i) => i / new ByteVector4(b[y, x]));

    public static ColorMatrix4 operator /(ColorMatrix4 a, IMatrix2D<ByteVector4> b) => a.New((y, x, i) => i / b[y, x]);

    public static ColorMatrix4 operator %(ColorMatrix4 a, IVector3<byte> b) => a.New(i => i % new ByteVector4(b));

    public static ColorMatrix4 operator %(ColorMatrix4 a, IMatrix2D<ByteVector3> b) => a.New((y, x, i) => i % new ByteVector4(b[y, x]));

    public static ColorMatrix4 operator %(ColorMatrix4 a, IMatrix2D<ByteVector4> b) => a.New((y, x, i) => i % b[y, x]);

    #endregion

    /// <see cref="IArray2D"/>

    object[][] IArray2D.ToArray() => XArray2D.ToArray(_Value, i => (object)i);

    public ByteVector4[][] ToArray() => [.. _Value];

    public ByteVector4[] ToArray(int y) => [.. _Value[y]];

    /// <see cref="IArray{,}"/>

    Array IArray<ColorMatrix4, ByteVector4>.GetArray() => new ByteVector4[Rows][];

    static ColorMatrix4 IArray<ColorMatrix4, ByteVector4>.Create(ColorMatrix4 oldSelf, Array newSelf) => new(newSelf as ByteVector4[][]);

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<ByteVector4>).GetEnumerator();

    readonly IEnumerator<ByteVector4> IEnumerable<ByteVector4>.GetEnumerator() { foreach (ByteVector4[] y in _Value) { foreach (ByteVector4 x in y) { yield return x; } } }

    /// <see cref="IFormattable"/>

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public readonly string ToString(string format, IFormatProvider provider) => IMatrix.ToString(this, format, provider);
}