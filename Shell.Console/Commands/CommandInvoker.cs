using Shell.Attributes;
using System.Reflection;

namespace Shell.Commands;

internal class CommandInvoker
{
    public static void Invoke(string command, string[] args)
    {
        var type = typeof(CommandHandlerV2);

        var instance = (CommandHandlerV2)Activator.CreateInstance(type)!;

        var methods = type.GetMethods(
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.DeclaredOnly);

        MethodInfo? matchingMethod = null;

        foreach (var method in methods)
        {
            var attributes = method.GetCustomAttributes<CommandAttribute>();

            foreach (var attribute in attributes)
            {
                if (attribute is CommandAttribute commandAttribute && commandAttribute.Command == command)
                {
                    matchingMethod = method;
                    break;
                }
            }

            if (matchingMethod is not null)
            {
                break;
            }
        }

        if (matchingMethod is not null)
        {
            _ = matchingMethod.Invoke(instance, [args]);
        }
        else
        {
            Console.WriteLine($"No method found for command: {command}");
        }

    }
}
