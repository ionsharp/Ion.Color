using System;

namespace Ion.Colors;

public enum ColorInterpolation
{
    /// <summary>Linear interpolation in 2D using the average of 3 neighboring pixels.</summary>
    Bilinear,
    /// <summary>The nearest neighbor algorithm simply selects the color of the nearest pixel.</summary>
    NearestNeighbor
}