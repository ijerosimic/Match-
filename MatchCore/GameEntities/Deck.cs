using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchCore.GameEntities
{
    public class Deck
    {
        public Deck(int noPacks)
        {
            for (var i = 0; i < noPacks; i++) BuildDeck();
        }
        
        private void BuildDeck()
        {
            foreach (var suit in Enum.GetValues<Suit>())
            foreach (var cardValue in Enum.GetValues<Value>())
                Cards.Add(new Card(cardValue, suit));
        }

        public List<Card> Cards { get; } = new();

        public List<Card> ShuffledCards => Cards.OrderBy(x => new Random().Next()).ToList();
    }
}