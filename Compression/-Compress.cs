using System;

namespace Ion.Colors;

/// <summary>Functions used for conversion to <see cref="XYZ"/> and back.</summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
public interface ICompress : IFormattable
{
    /// <summary>The compressed/companded channel (non linear) is made linear with respect to the energy.</summary>
    /// <remarks>Non Linear (Compressed/Companded) > Linear (Uncompressed/Uncompanded)</remarks>
    double TransferInverse(double channel);

    /// <summary>The uncompressed/uncompanded channel (linear) is made non linear.</summary>
    /// <remarks>Linear (Uncompressed/Uncompanded) > Non Linear (Compressed/Companded)</remarks>
    double Transfer(double channel);
}