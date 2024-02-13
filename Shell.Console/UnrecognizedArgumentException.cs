namespace Shell;

internal sealed class UnrecognizedArgumentException(string argument) : Exception($"Unrecognized argument: {argument}")
{
    public string Argument { get; } = argument;
}