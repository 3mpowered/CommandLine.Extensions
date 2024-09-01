using CommandDotNet;
using Empowered.CommandLine.Extensions.Extensions;
using FluentAssertions;
using Spectre.Console;

namespace Empowered.CommandLine.Extensions.Tests;

public class EmpoweredAppRunnerTests
{
    public class TestCommand(IAnsiConsole console)
    {
        [DefaultCommand]
        public async Task<int> Test([Option]bool error)
        {
            if (error)
            {
                throw new InvalidOperationException("Error");
            }
            console.Success("Hurra!");
            return await ExitCodes.Success;
        }
    }

    [Fact]
    public void ShouldRunConsoleApplication()
    {
        var runner = new EmpoweredAppRunner<TestCommand>("test");
        runner
            .Run()
            .Should()
            .Be(0);
    }

    [Fact]
    public void ShouldExitWithErrorOnException()
    {
        new EmpoweredAppRunner<TestCommand>("test")
            .Run(["--error"])
            .Should()
            .Be(1);
    }
}
