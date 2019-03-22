using PlayingCardsDeck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerApp
{
    internal class Hand
    {
        internal List<PlayingCard> Cards { get; set; }
        internal HandRankEnum Rank { get => DetermineRank(); }
        internal PlayingCard HighCard { get => Cards.OrderBy(card => card.Value).LastOrDefault(); }

        internal Hand(List<PlayingCard> cards)
        {
            Cards = cards;
        }

        private HandRankEnum DetermineRank()
        {
            throw new NotImplementedException();

            List<PlayingCard> tempHand = new List<PlayingCard>(Cards);

            // TODO: determine hand Strength/Rank

            return HandRankEnum.HighCard;
        }
    }
}
