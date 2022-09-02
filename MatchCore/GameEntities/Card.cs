namespace MatchCore.GameEntities
{
    public class Card
    {
        public Card(Value value, Suit suit)
        {
            Value = value;
            Suit = suit;
        }
        
        public Value Value { get; }
        public Suit Suit { get; }
    }
}