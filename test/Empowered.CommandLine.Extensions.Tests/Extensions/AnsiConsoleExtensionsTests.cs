using Empowered.CommandLine.Extensions.Extensions;
using FluentAssertions;
using Spectre.Console;
using Spectre.Console.Testing;

namespace Empowered.CommandLine.Extensions.Tests.Extensions;

public class AnsiConsoleExtensionsTests
{
    private const string Text = "test";
    private readonly IAnsiConsole _ansiConsole = new TestConsole();
    private readonly Recorder _recorder;

    public AnsiConsoleExtensionsTests()
    {
        _recorder = new Recorder(_ansiConsole);
    }

    [Fact]
    public void ShouldPrintErrorsInRed()
    {
        var console = new TestConsole();
        var recorder = new Recorder(console);
        recorder.Error(Text);

        recorder
            .ExportHtml()
            .Should()
            .Contain("style=\"color: #FF0000\"");
    }

    [Fact]
    public void ShouldPrintWarningsInYellow()
    {
        var console = new TestConsole();
        var recorder = new Recorder(console);
        recorder.Warning(Text);

        recorder
            .ExportHtml()
            .Should()
            .Contain("style=\"color: #FFFF00\"");
    }

    [Fact]
    public void ShouldPrintSuccessMessagesInGreen()
    {
        var console = new TestConsole();
        var recorder = new Recorder(console);
        recorder.Success(Text);

        recorder
            .ExportHtml()
            .Should()
            .Contain("style=\"color: #008000\"");
    }

    [Fact]
    public void ShouldPrintInfosInWhite()
    {
        var console = new TestConsole();
        var recorder = new Recorder(console);
        recorder.Info(Text);

        recorder
            .ExportHtml()
            .Should()
            .Contain("style=\"color: #FFFFFF\"");
    }
}
