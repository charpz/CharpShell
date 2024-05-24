using Shell.Attributes;
using Shell.Utils;

namespace Shell.Commands;

public class CommandHandlerV2
{
    [Command("cd")]
    public void ChangeDirectoryMethod(string[] args)
    {
        if (args.Length is 0)
        {
            Directory.SetCurrentDirectory(ShellEnvironment.DirectoryRoot);
        }
        else if (args.Length > 0)
        {
            var destDirectory = Path.Combine(Environment.CurrentDirectory, args[0]);

            if (!Path.Exists(destDirectory))
            {
                Console.WriteLine($"Cannot find path '{destDirectory}' because it does not exist.");
            }

            Environment.CurrentDirectory = destDirectory;

        }
    }

    [Command("ls")]
    public void ListMethod(string[] args)
    {
        if (args.Length is 0)
        {
            Console.WriteLine(Environment.CurrentDirectory);
            var directories = FileUtility.ListCurrentDirectory();

            foreach (var directory in directories)
            {
                Console.WriteLine(directory);
            }
        }
        else if (args.First() is "-la")
        {
            Console.WriteLine(Environment.CurrentDirectory);

            var directories = FileUtility.ListCurrentDirectoryWithDetails();

            foreach (var directory in directories)
            {
                Console.WriteLine(directory.Name + "    " + "Last Write Time: " + directory.LastWrite);
            }
        }
        else
        {
            throw new UnrecognizedArgumentException(args.First());
        }
    }
}