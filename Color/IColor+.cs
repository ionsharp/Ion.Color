using Ion.Numeral;
using System;
using System.Collections.Generic;

namespace Ion.Colors;

/// <inheritdoc/>
public interface IColor<T> : IColor, IFormattable, IVectorMutable<T>
{
    /// <remarks><b>Illogical to support!</b></remarks>
    T IArray<T>.this[int index] => throw new NotSupportedException();
}

/// <inheritdoc/>
public interface IColor<TSelf, TValue> : IColor<TValue>, IVector<TSelf, TValue> where TSelf : IColor<TSelf, TValue>
{
    Array IArray<TSelf, TValue>.GetArray() => default; //ToArray();

    [Obsolete]
    static TSelf IVector<TSelf, TValue>.Create(VectorType type, IEnumerable<TValue> value) => default;
}