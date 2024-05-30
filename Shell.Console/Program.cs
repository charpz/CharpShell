using Shell;
using Shell.Commands;
using Shell.Utils;
using System.Diagnostics;

Directory.SetCurrentDirectory(ShellEnvironment.DirectoryRoot);

new Thread(MainLoop).Start();
new Thread(SafeExitThread).Start();

static void MainLoop()
{
    var stopwatch = new Stopwatch();

    while (true)
    {
        Prompt.Ready();

        var input = Console.ReadLine();

        if (String.IsNullOrWhiteSpace(input)) continue;

        stopwatch.Restart();

        try
        {
            var (command, args) = Parser.InputParse(input);

            Command.Invoke(command, [.. args]);
        }
        catch (UnrecognizedCommandException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (UnrecognizedArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

        stopwatch.Stop();
        Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} milliseconds");
    }
}

static void SafeExitThread()
{
    while (true)
    {
        Console.CancelKeyPress += (sender, e) =>
        {
            if (e.SpecialKey == ConsoleSpecialKey.ControlC || e.SpecialKey == ConsoleSpecialKey.ControlBreak)
            {
                Console.WriteLine("Ctrl+C or Ctrl+Break detected. Exiting...");
                Environment.Exit(0);
            }
        };

        // Delay to avoid excessive CPU usage.
        Thread.Sleep(100);
    }
}

file struct Prompt
{
    public static void Ready()
    {
        Console.WriteLine(Environment.CurrentDirectory);
        Console.Write("> ");
    }
}