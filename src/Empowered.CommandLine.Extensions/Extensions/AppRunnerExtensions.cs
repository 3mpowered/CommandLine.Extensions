using System.Diagnostics;
using CommandDotNet;
using CommandDotNet.Builders.ArgumentDefaults;
using Microsoft.Extensions.Configuration;

namespace Empowered.CommandLine.Extensions.Extensions;

public static class AppRunnerExtensions
{
    [Conditional("DEBUG")]
    public static void AddDebugExtensions(this AppRunner appRunner)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("local.settings.json", optional: true)
            .Build();

        appRunner
            .UseDebugDirective()
            .UseParseDirective()
            .UseCommandLogger()
            .UseResponseFiles()
            .UseLocalizeDirective()
            .UseTimeDirective()
            .UseDefaultsFromConfig(
                DefaultSources.GetValueFunc(nameof(DefaultSources.AppSetting),
                    key => configuration[key],
                    DefaultSources.AppSetting.GetKeyFromAttribute
                )
            );
    }
}
