using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <inheritdoc/>
public interface IColor4<T> : IColor4, IColor3<T>, IVector4<T>
{    
    /// <remarks><b>Required for most specific implementation.</b></remarks>
    T IArray<T>.this[int index] => throw new NotSupportedException();

    /// <summary>
    /// Get and set <see cref="Component4.W"/>.
    /// </summary>
    new public T W { get; set; }

    /// <summary>
    /// Get and set <see cref="Component4.X"/>, <see cref="Component4.Y"/>, <see cref="Component4.Z"/>, and <see cref="Component4.W"/>.
    /// </summary>
    new public Vector4<T> XYZW { get; set; }

    (T X, T Y) IVector4<T>.XY => (X, Y);

    (T X, T Y, T Z) IVector4<T>.XYZ => (X, Y, Z);
}

/// <inheritdoc/>
public interface IColor4<TSelf, TValue> : IColor3<TSelf, TValue>, IColor4<TValue>, IVector<TSelf, TValue> where TSelf : IColor4<TSelf, TValue>
{
    static TSelf IArray<TSelf, TValue>.Create(TSelf oldSelf, Array newSelf)
    {
        oldSelf.XYZW = new Vector4<TValue>((TValue)newSelf.GetValue(0), (TValue)newSelf.GetValue(1), (TValue)newSelf.GetValue(2), (TValue)newSelf.GetValue(3));
        return oldSelf;
    }
}