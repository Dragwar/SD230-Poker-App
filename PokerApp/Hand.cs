using PlayingCardsDeck;
using PokerApp.CardModels;
using System.Collections.Generic;
using System.Linq;

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
            //List<PlayingCard> tempHand = new List<PlayingCard>(Cards);

            // just for testing hands
            List<PlayingCard> tempHand = new List<PlayingCard>()
            {
                new PlayingCard(CardNameValueEnum.Ace, SuitEnum.Clubs),
                new PlayingCard(CardNameValueEnum.Ace, SuitEnum.Hearts),
                new PlayingCard(CardNameValueEnum.Queen, SuitEnum.Clubs),
                new PlayingCard(CardNameValueEnum.Queen, SuitEnum.Spades),
                new PlayingCard(CardNameValueEnum.Queen, SuitEnum.Diamonds),
            };

            List<HandRankEnum> tempRanks = new List<HandRankEnum>();

            // TODO: determine hand Strength/Rank
            //var a = GetCardCountNameValueList(tempHand);
            var a = GetCardCountNameValueList(Cards);

            HashSet<CardCountNameValue> onePairCards = new HashSet<CardCountNameValue>();
            CardCountNameValue threeOfAKind = null;
            CardCountNameValue fourOfAKind = null;
            CardCountNameValue fiveOfAKind = null;

            foreach (CardCountNameValue item in a)
            {
                switch (item.CardCount)
                {
                    case 2: onePairCards.Add(item); break;
                    case 3: threeOfAKind = item; break;
                    case 4: fourOfAKind = item; break;
                    case 5: fiveOfAKind = item; break;
                }
            }

            if (onePairCards.Count == 1)
            {
                tempRanks.Add(HandRankEnum.OnePair);
            }
            else if (onePairCards.Count == 2)
            {
                tempRanks.Add(HandRankEnum.TwoPair);
            }
            else if (threeOfAKind != null)
            {
                tempRanks.Add(HandRankEnum.ThreeOfAKind);
            }
            else if (fourOfAKind != null)
            {
                tempRanks.Add(HandRankEnum.FourOfAKind);
            }
            else if (fiveOfAKind != null)
            {
                tempRanks.Add(HandRankEnum.FiveOfAKind);
            }

            if (onePairCards.Count == 1 && threeOfAKind != null)
            {
                tempRanks.Add(HandRankEnum.FullHouse);
            }

            HandRankEnum handRank = tempRanks.Any() ? tempRanks.Max() : HandRankEnum.HighCard;
            return handRank;
        }

        public List<CardCountNameValue> GetCardCountNameValueList(List<PlayingCard> cards) => (
            cards.GroupBy(card => card.Value)
                 .Select(group => new CardCountNameValue() { CardName = (CardNameValueEnum)group.Key, CardValue = group.Key, CardCount = group.Count() })
                 .ToList()
        );


        //private HandRankEnum CheckForOnePair(HandRankEnum currentRank)
        //{
        //    ICollection<dynamic> b = GetCountOfCardValue();// Array of objects
        //    var a = Cards.GroupBy(card => card.Value).ToList();
        //    var c = b.ToList()[0].CardName
        //    //foreach (var item in b)
        //    //{
        //    //    switch (item)
        //    //    {

        //    //        default:
        //    //            break;
        //    //    }
        //    //}
        //}
    }
}
