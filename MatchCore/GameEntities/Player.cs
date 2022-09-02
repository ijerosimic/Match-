using System.Collections.Generic;

namespace MatchCore.GameEntities
{
    public interface IPlayer
    {
        IEnumerable<IPlayer> GetPlayers(int noOfPlayers);
        void UpdateNumberOfCardsHeld(int cardsWon);
        int GetNumberOfCardsHeld();
        int GetPlayerNumber();
    }

    public class Player : IPlayer
    {
        public IEnumerable<IPlayer> GetPlayers(int noOfPlayers)
        {
            for (var i = 1; i <= noOfPlayers; i++)
                yield return new Player { Number = i };
        }

        public void UpdateNumberOfCardsHeld(int cardsWon) => CardsHeld += cardsWon;
        public int GetNumberOfCardsHeld() => CardsHeld;
        public int GetPlayerNumber() => Number;

        private int Number { get; init; }
        private int CardsHeld { get; set; }
    }
}