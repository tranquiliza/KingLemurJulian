using KingLemurJulian.Core;
using KingLemurJulian.Core.Commands;
using KingLemurJulian.Sql;
using KingLemurJulian.TwitchIntegration;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace KingLemurJulian.BackgroundHost
{
    public static class DependencyInjection
    {
        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(builder =>
            {
                var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
                if (string.Equals(environmentName, "development", StringComparison.OrdinalIgnoreCase))
                    builder.AddConsole();
                else
                    builder.AddSeq(configuration.GetSection("Seq"));
            });

            var twitchChatSettings = new TwitchClientSettings(configuration.GetRequiredValue<string>("Twitch:Username"), configuration.GetRequiredValue<string>("Twitch:OAuth"));

            var connectionString = configuration.GetRequiredValue<string>("ConnectionStrings:SqlDatabase");

            services.RegisterBot();

            services.AddSingleton<IBotRunner, BotRunner>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IChannelRepository, ChannelRepository>(_ => new ChannelRepository(connectionString));
            services.AddHostedService<BotService>();

            services.AddMediatR(typeof(DependencyInjection).GetTypeInfo().Assembly, typeof(ICommandExecutor).GetTypeInfo().Assembly);

            var container = services.BuildServiceProvider();

            var twitchChatClient = new TwitchChatClient(twitchChatSettings, container.GetRequiredService<ILogger<TwitchChatClient>>());
            services.AddSingleton<IChatClient>(twitchChatClient);
            services.AddSingleton<IChatMessageSender>(twitchChatClient);
        }
    }
}
