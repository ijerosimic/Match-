using System;
using MatchCore.GameLogic;
using MatchCore.Helpers;

namespace MatchCore;

public class App
{
    private readonly IWriter _writer;
    private readonly IMatchGame _matchGame;

    public App(IMatchGame matchGame, IWriter writer)
    {
        _matchGame = matchGame;
        _writer = writer;
    }

    public void Run(string[] args)
    {
        if (args?.Length != 2)
            throw new ArgumentException("Invalid args lenght (!2)");

        var noPacks = int.Parse(args[0]);
        var matchCondition = Enum.Parse<MatchCondition>(args[1]);

        var winner = _matchGame.PlayAndReturnWinner(2, noPacks, matchCondition);
        var message = winner == -1 ? "Game ended in a draw." : $"Player {winner} is the winner.";
        _writer.WriteToConsole(message);
    }
}