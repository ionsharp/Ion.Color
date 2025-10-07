using Ion.Analysis;
using Ion.Core;
using System;

namespace Ion.Colors;

public abstract record class ColorAnalysis() : Model(), IAnalysis
{
    public string Model { get => Get<string>(); set => Set(value); }

    protected ColorAnalysis(string model) : this() => Model = model;

    public override string ToString(string format, IFormatProvider provider) => $"{Model}\n";
}

public record class ColorAccuracyAnalysis : ColorAnalysis
{
    public string Accuracy { get => Get<string>(); set => Set(value); }

    public ColorAccuracyAnalysis(string model, object accuracy) : base(model)
    {
        Accuracy = $"{accuracy}";
    }

    public override string ToString(string format, IFormatProvider provider) => $"{base.ToString()}{Accuracy}";
}

public record class ColorRangeAnalysis : ColorAccuracyAnalysis
{
    public string Maximum { get => Get<string>(); set => Set(value); }

    public string Minimum { get => Get<string>(); set => Set(value); }

    public ColorRangeAnalysis(string model, object accuracy, object minimum, object maximum) : base(model, accuracy)
    {
        Minimum = Infinite($"{minimum}"); Maximum = Infinite($"{maximum}");
    }

    public override string ToString(string format, IFormatProvider provider) => $"{base.ToString()}\n{Minimum}, {Maximum}";

    protected static string Infinite(string input) => input.Replace("999", "∞").Replace("-999", "∞");
}

public record class ColorRangeInverseAnalysis : ColorRangeAnalysis
{
    public string TargetMaximum { get => Get(""); set => Set(value); }

    public string TargetMinimum { get => Get(""); set => Set(value); }

    public ColorRangeInverseAnalysis(string model, object accuracy, object minimum, object maximum, object targetMinimum, object targetMaximum) : base(model, accuracy, minimum, maximum)
    {
        TargetMinimum = Infinite($"{targetMinimum}"); TargetMaximum = Infinite($"{targetMaximum}");
    }

    public override string ToString(string format, IFormatProvider provider) => $"{base.ToString()}\n{TargetMinimum}, {TargetMaximum}";
}