using System;
using MatchCore.GameEntities;
using MatchCore.GameLogic;
using MatchCore.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace MatchCore
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var services = BuildServices();
            var app = services.GetRequiredService<App>();
            app.Run(args);
        }

        private static IServiceProvider BuildServices() =>
            new ServiceCollection()
                .AddSingleton<App>()
                .AddSingleton<IWriter, Writer>()
                .AddSingleton<ICardMatcher, CardMatcher>()
                .AddSingleton<IPlayer, Player>()
                .AddSingleton<IMatchGame, MatchGame>()
                .BuildServiceProvider();
    }
}