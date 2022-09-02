using System.Collections.Generic;
using FluentAssertions;
using MatchCore.GameEntities;
using MatchCore.GameLogic;
using Xunit;

namespace MatchCoreTests
{
    public class CardMatcherTests
    {
        [Theory]
        [MemberData(nameof(SuitEqualityTestData))]
        public void It_Returns_Correct_MatchCondition_When_Param_Is_Suit(Card card1, Card card2, bool isEqual)
        {
            var isMatch = new CardMatcher().IsMatch(MatchCondition.Suits, card1, card2);
            isMatch.Should().Be(isEqual);
        }

        [Theory]
        [MemberData(nameof(ValueEqualityTestData))]
        public void It_Returns_Correct_MatchCondition_When_Param_Is_Value(Card card1, Card card2, bool isEqual)
        {
            var isMatch = new CardMatcher().IsMatch(MatchCondition.Values, card1, card2);
            isMatch.Should().Be(isEqual);
        }

        [Theory]
        [MemberData(nameof(BothEqualityTestData))]
        public void It_Returns_Correct_MatchCondition_When_Param_Is_Both(Card card1, Card card2, bool isEqual)
        {
            var isMatch = new CardMatcher().IsMatch(MatchCondition.Both, card1, card2);
            isMatch.Should().Be(isEqual);
        }

        public static IEnumerable<object[]> SuitEqualityTestData => new List<object[]>
        {
            //same suit
            new object[] { new Card(Value.King, Suit.Clubs), new Card(Value.Ace, Suit.Clubs), true },

            //same suit and value
            new object[] { new Card(Value.Ace, Suit.Clubs), new Card(Value.Ace, Suit.Clubs), true },

            //different suit, same value
            new object[] { new Card(Value.Ace, Suit.Clubs), new Card(Value.Ace, Suit.Hearts), false },

            //different suit and value
            new object[] { new Card(Value.King, Suit.Hearts), new Card(Value.Ace, Suit.Clubs), false }
        };

        public static IEnumerable<object[]> ValueEqualityTestData => new List<object[]>
        {
            //same suit
            new object[] { new Card(Value.King, Suit.Clubs), new Card(Value.Ace, Suit.Clubs), false },

            //same suit and value
            new object[] { new Card(Value.Ace, Suit.Clubs), new Card(Value.Ace, Suit.Clubs), true },

            //different suit, same value
            new object[] { new Card(Value.Ace, Suit.Clubs), new Card(Value.Ace, Suit.Hearts), true },

            //different suit and value
            new object[] { new Card(Value.King, Suit.Hearts), new Card(Value.Ace, Suit.Clubs), false }
        };

        public static IEnumerable<object[]> BothEqualityTestData => new List<object[]>
        {
            //same suit
            new object[] { new Card(Value.King, Suit.Clubs), new Card(Value.Ace, Suit.Clubs), false },

            //same suit and value
            new object[] { new Card(Value.Ace, Suit.Clubs), new Card(Value.Ace, Suit.Clubs), true },

            //different suit, same value
            new object[] { new Card(Value.Ace, Suit.Clubs), new Card(Value.Ace, Suit.Hearts), false },

            //different suit and value
            new object[] { new Card(Value.King, Suit.Hearts), new Card(Value.Ace, Suit.Clubs), false }
        };
    }
}