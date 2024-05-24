namespace Shell.Utils;

internal static class Parser
{
    public static (string command, string[] arguments) InputParse(ReadOnlySpan<char> input)
    {
        input = input.Trim();

        var parts = Split(input);

        // The first part is the command, the rest are arguments.
        var command = parts[0];
        var arguments = new string[parts.Length - 1];
        for (int i = 1; i < parts.Length; i++)
        {
            arguments[i - 1] = parts[i];
        }

        return (command.ToString(), arguments);
    }

    private static ReadOnlySpan<string> Split(ReadOnlySpan<char> input)
    {
        var parts = new List<string>();
        int wordStart = -1;

        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsWhiteSpace(input[i]))
            {
                if (wordStart != -1)
                {
                    parts.Add(input.Slice(wordStart, i - wordStart).ToString());
                    wordStart = -1;
                }
            }
            else
            {
                if (wordStart == -1)
                {
                    wordStart = i;
                }
            }
        }

        if (wordStart != -1)
        {
            parts.Add(input.Slice(wordStart).ToString());
        }

        return new ReadOnlySpan<string>(parts.ToArray());
    }

}

//public static (string command, List<string> arguments) InputParse(string input)
//{
//    var parts = input.Trim().Split();

//    return (parts[0], parts.Skip(1).ToList());
//}

