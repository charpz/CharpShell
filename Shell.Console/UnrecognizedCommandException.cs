namespace Shell;
internal sealed class UnrecognizedCommandException(string command) : Exception($"Unrecognized command: {command}")
{
    public string Command { get; } = command;
}