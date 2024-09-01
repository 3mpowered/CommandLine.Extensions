namespace Empowered.CommandLine.Extensions.Extensions;

public static class StringExtensions
{
    public static string Bold(this string text) => $"[bold]{text}[/]";
    public static string Dim(this string text) => $"[dim]{text}[/]";
    public static string Italic(this string text) => $"[italic]{text}[/]";
    public static string Underline(this string text) => $"[underline]{text}[/]";
    public static string Invert(this string text) => $"[invert]{text}[/]";
    public static string Conceal(this string text) => $"[conceal]{text}[/]";
    public static string SlowBlink(this string text) => $"[slowblink]{text}[/]";
    public static string RapidBlink(this string text) => $"[rapidblink]{text}[/]";
    public static string Strikethrough(this string text) => $"[strikethrough]{text}[/]";
    public static string Link(this string text) => $"[link]{text}[/]";
}
