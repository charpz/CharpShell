using Shell.Attributes;

namespace Shell.Commands;

public class CommandHandlerV2
{
    [Command("cd")]
    public void CommandMethod()
    {
        Console.WriteLine("Hello, this is the command method.");
    }

    [Command("ls")]
    public void ListMethod()
    {
        Console.WriteLine("Listing items...");
    }
}