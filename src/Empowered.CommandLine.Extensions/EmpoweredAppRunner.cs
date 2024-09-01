using CommandDotNet;
using CommandDotNet.Builders;
using CommandDotNet.Builders.ArgumentDefaults;
using CommandDotNet.Execution;
using CommandDotNet.Help;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using CommandDotNet.NameCasing;
using CommandDotNet.Spectre;
using Empowered.CommandLine.Extensions.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Spectre.Console;

namespace Empowered.CommandLine.Extensions;

public class EmpoweredAppRunner<TRootCommand> : AppRunner<TRootCommand> where TRootCommand : class
{
    public EmpoweredAppRunner(string appName, Action<IServiceCollection, IConfigurationBuilder>? configure = null)
    {
        AppSettings.Help.UsageAppName = appName;
        AppSettings.Help.UsageAppNameStyle = UsageAppNameStyle.Adaptive;
        var configurationBuilder = new ConfigurationBuilder();
        var serviceCollection = new ServiceCollection();

        configure?.Invoke(serviceCollection, configurationBuilder);

        var configuration = configurationBuilder
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables($"{appName.ToUpperInvariant()}:")
            .Build();

        serviceCollection.TryAddSingleton<IConfiguration>(configuration);
        serviceCollection.TryAddSingleton(AnsiConsole.Console);

        foreach (var commandClassType in this.GetCommandClassTypes())
        {
            serviceCollection.TryAddScoped(commandClassType.type);
        }

        this
            .UseSpectreAnsiConsole()
            .UseSpectreArgumentPrompter()
            .UseDefaultsFromConfig(
                DefaultSources.GetValueFunc(
                    nameof(DefaultSources.EnvVar),
                    key => configuration[key],
                    DefaultSources.EnvVar.GetKeyFromAttribute
                )
            )
            .UseErrorHandler(ErrorHandler)
            .UseCancellationHandlers()
            .UseVersionMiddleware()
            .UseTypoSuggestions()
            .UseDefaultsFromConfig()
            .UseNameCasing(Case.KebabCase)
            .UseMicrosoftDependencyInjection(
                serviceCollection.BuildServiceProvider(),
                argumentModelResolveStrategy: ResolveStrategy.TryResolve,
                commandClassResolveStrategy: ResolveStrategy.ResolveOrThrow
            );
        this.AddDebugExtensions();
    }

    private static int ErrorHandler(CommandContext? context, Exception exception)
    {
        var errorCode = ExitCodes.Error.GetAwaiter().GetResult();

        if (context?.DependencyResolver == null)
        {
            return errorCode;
        }

        var console = context.DependencyResolver.Resolve<IAnsiConsole>() ?? AnsiConsole.Console;

        console.Error(exception.Message.EscapeMarkup());
        return errorCode;
    }
}
