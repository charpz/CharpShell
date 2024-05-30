using Shell.Attributes;
using System.Reflection;

namespace Shell.Commands;

internal static class Command
{
    private static readonly Type _type = typeof(CommandHandlerV2);
    private static readonly CommandHandlerV2 _instance = (CommandHandlerV2)Activator.CreateInstance(_type)!;
    private static readonly Dictionary<string, MethodInfo> _commandAttributes = ListCommadAttributes();

    public static void Invoke(string command, string[] args)
    {
        if (_commandAttributes.TryGetValue(command, out var method))
        {
            if (method is not null)
            {
                _ = method?.Invoke(_instance, [args]);
            }
            else
            {
                Console.WriteLine($"No method found for command: {command}");
            }
        }
    }

    private static Dictionary<string, MethodInfo> ListCommadAttributes()
    {
        var dic = new Dictionary<string, MethodInfo>();

        var methods = _type.GetMethods(
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.DeclaredOnly);

        foreach (var method in methods)
        {
            var attribute = method.GetCustomAttribute<CommandAttribute>();

            if (attribute is not null)
            {
                dic.Add(attribute.Command, method);
            }
        }

        return dic;
    }
}