using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <inheritdoc/>
public interface IColor2<T> : IColor2, IColor<T>, IVector2<T>
{
    /// <remarks><b>Required for most specific implementation.</b></remarks>
    T IArray<T>.this[int index] => throw new NotSupportedException();

    /// <summary>
    /// Get and set <see cref="Component2.X"/>.
    /// </summary>
    new public T X { get; set; }

    /// <summary>
    /// Get and set <see cref="Component2.Y"/>.
    /// </summary>
    new public T Y { get; set; }

    /// <summary>
    /// Get and set <see cref="Component2.X"/> and <see cref="Component2.Y"/>.
    /// </summary>
    new public Vector2<T> XY { get; set; }
}

/// <inheritdoc/>
public interface IColor2<TSelf, TValue> : IColor<TSelf, TValue>, IColor2<TValue>, IVector<TSelf, TValue> where TSelf : IColor2<TSelf, TValue>
{
    static TSelf IArray<TSelf, TValue>.Create(TSelf oldSelf, Array newSelf)
    {
        oldSelf.XY = new Vector2<TValue>((TValue)newSelf.GetValue(0), (TValue)newSelf.GetValue(1));
        return oldSelf;
    }
}