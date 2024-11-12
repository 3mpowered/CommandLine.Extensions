using Spectre.Console;

namespace Empowered.CommandLine.Extensions.Extensions;

public static class AnsiConsoleExtensions
{

    public static void Info(this IAnsiConsole console, string message) =>
        console.MarkupLineInterpolated($"[white]{message}[/]");

    public static void Success(this IAnsiConsole console, string message) =>
        console.MarkupLineInterpolated($"[green]{message}[/]");

    public static void Warning(this IAnsiConsole console, string message) =>
        console.MarkupLineInterpolated($"[yellow]{message}[/]");

    public static void Error(this IAnsiConsole console, string message) =>
        console.MarkupLineInterpolated($"[red]{message}[/]");
}
