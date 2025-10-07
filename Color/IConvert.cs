namespace Ion.Colors;

public interface IConvert<T> where T : IColor
{
    void From(in T input, ColorProfile profile);

    void To(out T result, ColorProfile profile);
}