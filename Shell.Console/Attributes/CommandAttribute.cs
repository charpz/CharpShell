namespace Shell.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
sealed class CommandAttribute : Attribute
{
    public string Command { get; }

    public CommandAttribute(string command)
    {
        Command = command;
    }
}