using System;
using System.Collections.Generic;
using System.Linq;
using MatchCore.GameEntities;

namespace MatchCore.GameLogic
{
    public interface IMatchGame
    {
        /// <summary>
        /// Plays a game of 'Match!' and returns the winning player number (> 0), or -1 in the case of draw
        /// </summary>
        /// <param name="noOfPlayers">Number of players</param>
        /// <param name="noOfPacks">Number of playing card packs (1 pack = 52 cards)</param>
        /// <param name="matchCondition">Card equality comparer</param>
        /// <returns>Winning player number</returns>
        int PlayAndReturnWinner(int noOfPlayers, int noOfPacks, MatchCondition matchCondition);
    }

    public class MatchGame : IMatchGame
    {
        private readonly IPlayer _player;
        private readonly ICardMatcher _cardMatcher;

        public MatchGame(IPlayer player, ICardMatcher cardMatcher)
        {
            _player = player;
            _cardMatcher = cardMatcher;
        }

        public int PlayAndReturnWinner(int noOfPlayers, int noOfPacks, MatchCondition matchCondition)
        {
            var players = _player.GetPlayers(noOfPlayers).ToList();
            var deck = new Deck(noOfPacks).ShuffledCards;
            var currentCard = deck.First();
            var pile = new List<Card>();

            while (deck.Count > 1)
            {
                DrawCard();

                if (pile.Count < 2) continue; //Nothing to compare with
                if (!IsMatch()) continue;

                var roundWinner = new Random().Next(noOfPlayers);
                MovePileToWinnersHand(roundWinner);
            }

            return GetResult(players);

            void DrawCard()
            {
                pile.Add(currentCard);
                deck.Remove(currentCard);
                currentCard = deck.First();
            }

            bool IsMatch() => _cardMatcher.IsMatch(matchCondition, currentCard, pile[^2]);

            void MovePileToWinnersHand(int roundWinner)
            {
                players[roundWinner].UpdateNumberOfCardsHeld(pile.Count);
                pile = new List<Card>();
            }
        }

        private static int GetResult(IEnumerable<IPlayer> players) =>
            players
                .Aggregate((player1, player2) =>
                    player1.GetNumberOfCardsHeld() > player2.GetNumberOfCardsHeld()
                        ? player1
                        : player1.GetNumberOfCardsHeld().Equals(player2.GetNumberOfCardsHeld())
                            ? null
                            : player2)?
                .GetPlayerNumber() ?? -1;
    }
}