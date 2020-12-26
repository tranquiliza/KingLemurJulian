using KingLemurJulian.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace KingLemurJulian.Core
{
    public static class BotRegistrations
    {
        public static void RegisterBot(this IServiceCollection services)
        {
            services.AddTransient<ICommandExecutor, PingCommand>();
            services.AddTransient<ICommandExecutor, JoinCommand>();
            services.AddTransient<ICommandExecutor, LeaveCommand>();

            services.AddTransient<ICommandExecutor, C2FCommand>();
            services.AddTransient<ICommandExecutor, Cm2FeetCommand>();

            services.AddTransient<IList<ICommandExecutor>>(x => new List<ICommandExecutor>(x.GetServices<ICommandExecutor>()));
        }
    }
}
