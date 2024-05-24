using Shell.Commands;
using Shell.Utils;

namespace Shell;

[Obsolete("Use " + nameof(CommandHandlerV2) + " instead.")]
internal static class CommandHandler
{
    [Obsolete]
    public static void Execute(string input)
    {
        var (command, arguments) = Parse(input);

        switch (command)
        {
            case "cd":
                if (arguments.Count is 0)
                {
                    Directory.SetCurrentDirectory(ShellEnvironment.DirectoryRoot);
                    break;
                }
                else if (arguments.Count > 0)
                {
                    var destDirectory = Path.Combine(Environment.CurrentDirectory, arguments[0]);

                    if (!Path.Exists(destDirectory))
                    {
                        Console.WriteLine($"Cannot find path '{destDirectory}' because it does not exist.");
                        break;
                    }

                    Environment.CurrentDirectory = destDirectory;

                    break;
                }
                else
                {
                    throw new UnrecognizedCommandException(command);
                }

            case "cd..":
                var parent = Directory.GetParent(Environment.CurrentDirectory);

                if (parent is null)
                    break;

                Environment.CurrentDirectory = parent.FullName;
                break;

            case "ls":
                if (arguments.Count is 0)
                {
                    Console.WriteLine(Environment.CurrentDirectory);
                    var directories = FileUtility.ListCurrentDirectory();

                    foreach (var directory in directories)
                    {
                        Console.WriteLine(directory);
                    }

                    break;
                }
                else if (arguments.First() is "-la")
                {
                    Console.WriteLine(Environment.CurrentDirectory);

                    var directories = FileUtility.ListCurrentDirectoryWithDetails();

                    foreach (var directory in directories)
                    {
                        Console.WriteLine(directory.Name + "    " + "Last Write Time: " + directory.LastWrite);
                    }

                    break;
                }
                else
                {
                    throw new UnrecognizedArgumentException(arguments.First());
                }

            case "cat":
                if (arguments.Count > 0)
                {
                    var destDirectory = Path.Combine(Environment.CurrentDirectory, arguments[0]);

                    if (!Path.Exists(destDirectory))
                    {
                        Console.WriteLine($"File not found with path {destDirectory}");
                        break;
                    }

                    var lines = File.ReadLines(destDirectory);

                    foreach (var line in lines)
                    {
                        Console.WriteLine(line);
                    }

                    break;
                }

                break;
            case "exit":
                Environment.Exit(0);
                break;

            default:
                throw new UnrecognizedCommandException(command);
        }
    }

    private static (string command, List<string> arguments) Parse(string input)
    {
        var parts = input.Trim().Split();

        return (parts[0], parts.Skip(1).ToList());
    }
}