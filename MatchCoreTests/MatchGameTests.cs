using System.Collections.Generic;
using FluentAssertions;
using MatchCore.GameEntities;
using MatchCore.GameLogic;
using Moq;
using Xunit;

namespace MatchCoreTests
{
    public class MatchGameTests
    {
        private readonly Mock<IPlayer> _p1Mock;
        private readonly Mock<IPlayer> _p2Mock;
        private readonly MatchGame _matchGame;

        public MatchGameTests()
        {
            _p1Mock = new Mock<IPlayer>();
            _p1Mock.Setup(p => p.GetPlayerNumber())
                .Returns(1);

            _p2Mock = new Mock<IPlayer>();
            _p2Mock.Setup(p => p.GetPlayerNumber())
                .Returns(2);

            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(p => p.GetPlayers(2))
                .Returns(new List<IPlayer>
                {
                    _p1Mock.Object,
                    _p2Mock.Object
                });

            var cardMatcherMock = new Mock<ICardMatcher>();
            cardMatcherMock
                .Setup(m => m.IsMatch(
                    It.IsAny<MatchCondition>(), It.IsAny<Card>(), It.IsAny<Card>()))
                .Returns(true);
            
            _matchGame = new MatchGame(playerMock.Object, cardMatcherMock.Object);
        }

        [Fact]
        public void It_Plays_A_2_Player_Game_And_Returns_The_Correct_WinningPlayer_Number()
        {
            //Winning player
            _p1Mock.Setup(p => p.GetNumberOfCardsHeld())
                .Returns(999);

            _p2Mock.Setup(p => p.GetNumberOfCardsHeld())
                .Returns(0);

            _matchGame.PlayAndReturnWinner(2, 1, MatchCondition.Both).Should().Be(1);
        }

        [Fact]
        public void It_Plays_A_2_Player_Game_And_Returns_NegativeOne_When_Result_Is_Draw()
        {
            _p1Mock.Setup(p => p.GetNumberOfCardsHeld())
                .Returns(999);

            _p2Mock.Setup(p => p.GetNumberOfCardsHeld())
                .Returns(999);

            _matchGame.PlayAndReturnWinner(2, 1, MatchCondition.Both).Should().Be(-1);
        }
    }
}