using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <inheritdoc/>
public interface IColor3<T> : IColor3, IColor2<T>, IVector3<T>
{
    /// <remarks><b>Required for most specific implementation.</b></remarks>
    T IArray<T>.this[int index] => throw new NotSupportedException();

    /// <summary>
    /// Get and set <see cref="Component3.Z"/>.
    /// </summary>
    new public T Z { get; set; }

    /// <summary>
    /// Get and set <see cref="Component3.X"/>, <see cref="Component3.Y"/>, and <see cref="Component3.Z"/>.
    /// </summary>
    new public Vector3<T> XYZ { get; set; }

    (T X, T Y) IVector3<T>.XY => (X, Y);
}

/// <inheritdoc/>
public interface IColor3<TSelf, TValue> : IColor2<TSelf, TValue>, IColor3<TValue>, IVector<TSelf, TValue> where TSelf : IColor3<TSelf, TValue>
{
    static TSelf IArray<TSelf, TValue>.Create(TSelf oldSelf, Array newSelf)
    {
        oldSelf.XYZ = new Vector3<TValue>((TValue)newSelf.GetValue(0), (TValue)newSelf.GetValue(1), (TValue)newSelf.GetValue(2));
        return oldSelf;
    }
}