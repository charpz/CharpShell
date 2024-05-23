namespace Shell;

internal static class ShellEnvironment
{
    public static string DirectoryRoot { get; } = Directory.GetDirectoryRoot(Environment.CurrentDirectory);
}
