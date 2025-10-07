using Ion.Numeral;

namespace Ion.Colors;

public record class ColorMatrixMutable4 : MatrixMutable<ByteVector4>
{
    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(int)"/>
    public ColorMatrixMutable4(int length) : this(length, length) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(int)"/>
    public ColorMatrixMutable4(int rows, int columns) : base(rows, columns, ByteVector4.Transparent) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(int, in T)"/>
    public ColorMatrixMutable4(int length, in IVector3<byte> color) : this(length, length, color) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(int, in T)"/>
    public ColorMatrixMutable4(int length, in IVector4<byte> color) : this(length, length, color) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(int, int, in T)"/>
    public ColorMatrixMutable4(int rows, int columns, in IVector3<byte> color) : base(rows, columns, new ByteVector4(color)) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(int, int, in T)"/>
    public ColorMatrixMutable4(int rows, int columns, in IVector4<byte> color) : base(rows, columns, new ByteVector4(color)) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in T[])"/>
    public ColorMatrixMutable4(in ByteVector3[] colors, Matrix2DFromArray1D fill = default) : base(colors?.ToArray(i => new ByteVector4(i)), fill) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in T[])"/>
    public ColorMatrixMutable4(in ByteVector4[] colors, Matrix2DFromArray1D fill = default) : base(colors, fill) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in T[][])"/>
    public ColorMatrixMutable4(in ByteVector3[][] colors) : base(colors?.ToArray(i => new ByteVector4(i))) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in T[][])"/>
    public ColorMatrixMutable4(in ByteVector4[][] colors) : base(colors) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in T[][][])"/>
    public ColorMatrixMutable4(in ByteVector3[][][] colors) : base(colors?.ToArray(i => new ByteVector4(i))) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in T[][][])"/>
    public ColorMatrixMutable4(in ByteVector4[][][] colors) : base(colors) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in IArray1D{T})"/>
    public ColorMatrixMutable4(in IArray1DRank<ByteVector3> colors, Matrix2DFromArray1D fill = default) : this(colors?.ToArray(i => new ByteVector4(i)), fill) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in IArray1D{T})"/>
    public ColorMatrixMutable4(in IArray1DRank<ByteVector4> colors, Matrix2DFromArray1D fill = default) : base(colors, fill) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in IArray2D{T})"/>
    public ColorMatrixMutable4(in IArray2DRank<ByteVector3> colors) : this(colors?.ToArray(i => new ByteVector4(i))) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in IArray2D{T})"/>
    public ColorMatrixMutable4(in IArray2DRank<ByteVector4> colors) : base(colors) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in IArray3D{T})"/>
    public ColorMatrixMutable4(in IArray3DRank<ByteVector3> colors) : this(colors?.ToArray(i => new ByteVector4(i))) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in IArray3D{T})"/>
    public ColorMatrixMutable4(in IArray3DRank<ByteVector4> colors) : base(colors) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in IMatrix2D{T})"/>
    public ColorMatrixMutable4(in IMatrix2D<ByteVector3> colors) : this(colors?.NewType(i => new ByteVector4(i))) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in IMatrix2D{T})"/>
    public ColorMatrixMutable4(in IMatrix2D<ByteVector4> colors) : base(colors) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in IMatrix3D{T})"/>
    public ColorMatrixMutable4(in IMatrix3D<ByteVector3> colors) : this(colors?.NewType(i => new ByteVector4(i))) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in IMatrix3D{T})"/>
    public ColorMatrixMutable4(in IMatrix3D<ByteVector4> colors) : base(colors) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in IVector{T})"/>
    public ColorMatrixMutable4(in IVector<ByteVector3> colors) : this(colors?.NewType(i => new ByteVector4(i)).ToArray()) { }

    /// <inheritdoc cref="MatrixMutable{T}.MatrixMutable(in IVector{T})"/>
    public ColorMatrixMutable4(in IVector<ByteVector4> colors) : base(colors) { }
}