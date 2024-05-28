namespace Shell.Utils;

internal static class Parser
{
    public static (string command, string[] arguments) InputParse(ReadOnlySpan<char> input)
    {
        Span<Range> parts = stackalloc Range[input.Length - 1];

        var numRegions = input.Trim().Split(parts, ' ', StringSplitOptions.TrimEntries);

        // The first region is the command, the rest are arguments.
        var command = input[parts[0].Start..parts[0].End];

        var numArgs = numRegions - 1;

        var arguments = new string[numArgs];

        for (int i = 1; i <= numArgs; i++)
        {
            arguments[i - 1] = input[parts[i].Start..parts[i].End].ToString();
        }

        return (command.ToString(), arguments);
    }
}