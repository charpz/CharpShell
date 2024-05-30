namespace Shell.Utils;

internal static class Parser
{
    public static (string command, string[] arguments) InputParse(ReadOnlySpan<char> input)
    {
        Span<Range> regions = stackalloc Range[input.Length - 1];

        var numRegions = input.Trim().Split(regions, ' ', StringSplitOptions.TrimEntries);

        // The first region is the command, the rest are arguments.
        var command = input[regions[0].Start..regions[0].End];

        var numArgs = numRegions - 1;

        var arguments = new string[numArgs];

        for (int i = 1; i <= numArgs; i++)
        {
            arguments[i - 1] = input[regions[i].Start..regions[i].End].ToString();
        }

        return (command.ToString(), arguments);
    }
}