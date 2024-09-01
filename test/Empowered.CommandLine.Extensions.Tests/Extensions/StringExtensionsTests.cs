using Empowered.CommandLine.Extensions.Extensions;
using FluentAssertions;

namespace Empowered.CommandLine.Extensions.Tests.Extensions;

public class StringExtensionsTests
{
    private const string Text = "test";

    [Fact]
    public void ShouldAddBoldMarkup() => Text
        .Bold()
        .Should()
        .Be($"[bold]{Text}[/]");

    [Fact]
    public void ShouldAddDimMarkup() => Text
        .Dim()
        .Should()
        .Be($"[dim]{Text}[/]");

    [Fact]
    public void ShouldAddItalicMarkup() => Text
        .Italic()
        .Should()
        .Be($"[italic]{Text}[/]");

    [Fact]
    public void ShouldAddUnderlineMarkup() => Text
        .Underline()
        .Should()
        .Be($"[underline]{Text}[/]");

    [Fact]
    public void ShouldAddInvertMarkup() => Text
        .Invert()
        .Should()
        .Be($"[invert]{Text}[/]");

    [Fact]
    public void ShouldAddConcealMarkup() => Text
        .Conceal()
        .Should()
        .Be($"[conceal]{Text}[/]");

    [Fact]
    public void ShouldAddSlowBlinkMarkup() => Text
        .SlowBlink()
        .Should()
        .Be($"[slowblink]{Text}[/]");

    [Fact]
    public void ShouldAddRapidBlinkMarkup() => Text
        .RapidBlink()
        .Should()
        .Be($"[rapidblink]{Text}[/]");

    [Fact]
    public void ShouldAddStrikethroughMarkup() => Text
        .Strikethrough()
        .Should()
        .Be($"[strikethrough]{Text}[/]");

    [Fact]
    public void ShouldAddLinkMarkup() => Text
        .Link()
        .Should()
        .Be($"[link]{Text}[/]");
}
