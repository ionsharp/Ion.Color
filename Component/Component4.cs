using System;

namespace Ion.Colors;

/// <summary>A <see cref="Component"/> of <see cref="IColor4"/>.</summary>
[Flags]
public enum Component4
{
    /// <summary>The 1st <see cref="Component"/> of <see cref="IColor4"/>.</summary>
    X,
    /// <summary>The 2nd <see cref="Component"/> of <see cref="IColor4"/>.</summary>
    Y,
    /// <summary>The 3rd <see cref="Component"/> of <see cref="IColor4"/>.</summary>
    Z,
    /// <summary>The 4th <see cref="Component"/> of <see cref="IColor4"/>.</summary>
    W
}