using PlayingCardsDeck;

namespace PokerApp.CardModels
{
    public class CardCountNameValue
    {
        public CardNameValueEnum CardName { get; set; }
        public int CardValue { get; set; }
        public int CardCount { get; set; }

        public override string ToString() => $"{CardName} - {CardValue} (Count: {CardCount})";
    }
}
