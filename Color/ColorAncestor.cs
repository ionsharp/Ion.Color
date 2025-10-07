using System;

namespace Ion.Colors;

/// <summary>
/// The <see cref="IColor"/> this instance is based on.
/// </summary>
/// <remarks>
/// <para><b>An <see cref="IColor"/> may be based on one other <see cref="IColor"/>.</b></para>
/// <para>See <see cref="IConvert{TColor}"/> to support conversion to an additional <see cref="IColor"/>.</para>
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1813:Avoid unsealed attributes")]
public class ColorOfAttribute(Type Type) : Attribute()
{
    public Type Type { get; } = Type;
}

/// <inheritdoc/>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ColorOfAttribute<T>() : ColorOfAttribute(typeof(T)) where T : IColor;