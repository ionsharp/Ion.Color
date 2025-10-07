using System;

namespace Ion.Colors;

/// <summary>Weighting parameters for CMC l:c color difference formula (<see cref="CMCColorDifference"/>).</summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
public enum CMCColorDifferenceThreshold
{
    /// <summary>2:1 (l:c).</summary>
    Acceptability,
    /// <summary>1:1 (l:c).</summary>
    Imperceptibility,
}