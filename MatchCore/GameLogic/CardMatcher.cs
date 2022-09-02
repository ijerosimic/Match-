using System;
using MatchCore.GameEntities;

namespace MatchCore.GameLogic
{
    public interface ICardMatcher
    {
        bool IsMatch(MatchCondition matchCondition, Card card1, Card card2);
    }

    public class CardMatcher : ICardMatcher
    {
        public bool IsMatch(MatchCondition matchCondition, Card card1, Card card2) => 
            GetFunc(matchCondition).Invoke(card1, card2);

        private static Func<Card, Card, bool> GetFunc(MatchCondition condition) =>
            condition switch
            {
                MatchCondition.Suits => MatchOnSuits,
                MatchCondition.Values => MatchOnValues,
                MatchCondition.Both => MatchOnBoth,
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };

        private static bool MatchOnSuits(Card card1, Card card2) => card1.Suit.Equals(card2.Suit);
        private static bool MatchOnValues(Card card1, Card card2) => card1.Value.Equals(card2.Value);

        private static bool MatchOnBoth(Card card1, Card card2) =>
            MatchOnSuits(card1, card2) && MatchOnValues(card1, card2);
    }

    public enum MatchCondition
    {
        Suits,
        Values,
        Both
    }
}