using System.CommandLine;
using Spectre.Console;

namespace scl;

class Program
{
    static async Task<int> Main(string[] args)
    {
        Option<int?> delayOption = new(
            name: "--delay",
            description: "The time in milliseconds to wait between printing each color. The default is 10ms.");

        var rootCommand = new RootCommand("Simple command that prints the colors in the ConsoleColor enum");
        rootCommand.AddOption(delayOption);

        rootCommand.SetHandler((delay) =>
            {
                PrintColors(delay ?? 10);
            },
            delayOption);

        return await rootCommand.InvokeAsync(args);
    }

    static void PrintColors(int delay)
    {
        foreach (ConsoleColor color in (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor)))
        {
            AnsiConsole.MarkupLine($"[{(int)color}]{color}[/]");
            Thread.Sleep(delay);
        }
    }
}