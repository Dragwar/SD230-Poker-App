using PlayingCardsDeck;

namespace PokerApp.CardModels
{
    public class CardCountSuit
    {
        public SuitEnum CardSuit { get; set; }
        public int CardCount { get; set; }

        public override string ToString() => $"{CardSuit} (Count: {CardCount})";
    }
}
