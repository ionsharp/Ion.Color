namespace Ion.Colors;

/// <summary>Compute distance between two colors.</summary>
/// <remarks><b>https://github.com/tompazourek/Colourful</b></remarks>
public interface IColorDifference
{
    /// <summary>Compute distance between two colors.</summary>
    public double ComputeDifference(in IColor x, in IColor y);
}

/// <inheritdoc/>
public interface IColorDifference<T> : IColorDifference where T : IColor
{
    /// <summary>Compute distance between two colors.</summary>
    public double ComputeDifference(in T x, in T y);
}