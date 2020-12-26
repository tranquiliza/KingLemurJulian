using KingLemurJulian.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace KingLemurJulian.Core
{
    public static class BotCommandRegistration
    {
        public static void RegisterBotCommands(this IServiceCollection services)
        {
            // Admin
            services.AddTransient<ICommandExecutor, JoinCommand>();
            services.AddTransient<ICommandExecutor, LeaveCommand>();

            // Base

            // Converters
            services.AddTransient<ICommandExecutor, C2FCommand>();
            services.AddTransient<ICommandExecutor, Cm2FeetCommand>();
            services.AddTransient<ICommandExecutor, Cm2InchesCommand>();
            services.AddTransient<ICommandExecutor, F2CCommand>();
            services.AddTransient<ICommandExecutor, Feet2CmCommand>();
            services.AddTransient<ICommandExecutor, Inches2CmCommand>();
            services.AddTransient<ICommandExecutor, Kilo2PoundCommand>();
            services.AddTransient<ICommandExecutor, Km2MilCommand>();
            services.AddTransient<ICommandExecutor, Km2MilesCommand>();
            services.AddTransient<ICommandExecutor, Mil2KmCommand>();
            services.AddTransient<ICommandExecutor, Miles2KmCommand>();
            services.AddTransient<ICommandExecutor, Pound2KiloCommand>();

            // Debug
            services.AddTransient<ICommandExecutor, PingCommand>();

            services.AddTransient<IList<ICommandExecutor>>(x => new List<ICommandExecutor>(x.GetServices<ICommandExecutor>()));
        }
    }
}
