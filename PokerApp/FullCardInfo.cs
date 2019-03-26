using PlayingCardsDeck;

namespace PokerApp
{
    public class FullCardInfo
    {
        public CardNameValueEnum Name { get; set; }
        public SuitEnum Suit { get; set; }
        public int CardCount { get; set; }
        public int Value { get => (int)Name; }

        public override string ToString() => $"{Name} - {Suit} - {Value}";
    }
}
