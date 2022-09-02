using System;
using FluentAssertions;
using MatchCore;
using MatchCore.GameLogic;
using MatchCore.Helpers;
using Moq;
using Xunit;

namespace MatchCoreTests;

public class AppTests
{
    private readonly Mock<IWriter> _writerMock;
    private readonly Mock<IMatchGame> _gameMock;

    public AppTests()
    {
        _writerMock = new Mock<IWriter>();
        _gameMock = new Mock<IMatchGame>();
    }

    [Fact]
    public void It_Throws_Exception_When_Args_Is_Invalid_Length()
    {
        var app = new App(_gameMock.Object, _writerMock.Object);

        var ex = Record.Exception(() => { app.Run(Array.Empty<string>()); });

        Assert.NotNull(ex);
        Assert.IsType<ArgumentException>(ex);
        ex.Message.Should().Be("Invalid args lenght (!2)");
    }

    [Fact]
    public void It_Calls_Game_Play_With_Correctly_Args_When_Args_Are_Valid()
    {
        var app = new App(_gameMock.Object, _writerMock.Object);

        app.Run(new[] { "1", "Both" });

        _gameMock.Verify(g =>
            g.PlayAndReturnWinner(2, 1, MatchCondition.Both), Times.Once);
    }

    [Fact]
    public void It_Outputs_Correct_Message_When_Winner_Is_Player_1()
    {
        _gameMock.Setup(g =>
                g.PlayAndReturnWinner(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MatchCondition>()))
            .Returns(1);
        
        var app = new App(_gameMock.Object, _writerMock.Object);

        app.Run(new[] { "1", "Both" });

        _writerMock.Verify(w => 
            w.WriteToConsole("Player 1 is the winner."), Times.Once);
    }
    
    [Fact]
    public void It_Outputs_Correct_Message_When_Result_Is_Draw()
    {
        _gameMock.Setup(g =>
                g.PlayAndReturnWinner(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MatchCondition>()))
            .Returns(-1);
        
        var app = new App(_gameMock.Object, _writerMock.Object);

        app.Run(new[] { "1", "Both" });

        _writerMock.Verify(w => 
            w.WriteToConsole("Game ended in a draw."), Times.Once);
    }
}