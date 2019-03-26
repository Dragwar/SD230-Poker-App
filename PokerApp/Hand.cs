using PlayingCardsDeck;
using PokerApp.CardModels;
using System;
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
            // just for testing hands
            //List<PlayingCard> testingHand = new List<PlayingCard>()
            //{
            //    new PlayingCard(CardNameValueEnum.Ace, SuitEnum.Clubs),
            //    new PlayingCard(CardNameValueEnum.Two, SuitEnum.Clubs),
            //    new PlayingCard(CardNameValueEnum.Ten, SuitEnum.Clubs),
            //    new PlayingCard(CardNameValueEnum.Queen, SuitEnum.Clubs),
            //    new PlayingCard(CardNameValueEnum.Eight, SuitEnum.Clubs),
            //};

            // all ranks the hand falls under
            List<HandRankEnum> tempRanks = new List<HandRankEnum>();


            //List<FullCardInfo> cards = GetCardCountNameValueList(testingHand);
            List<FullCardInfo> cards = GetListOfFullCardInfo(Cards);

            // (int Card Count, FullCardInfo Card)
            List<IGrouping<int, FullCardInfo>> cardGroupPairs = cards.GroupBy(card => card.CardCount).ToList();


            // Find pairs and XOfKinds
            foreach (IGrouping<int, FullCardInfo> item in cardGroupPairs)
            {
                switch (item.Key)
                {
                    // does hand contain a OnePair
                    case 2:

                        // does hand contain a TwoPair
                        if (item.ToList().Count == 4)
                        {
                            tempRanks.Add(HandRankEnum.TwoPair);
                        }

                        tempRanks.Add(HandRankEnum.OnePair);
                        break;

                    // is hand ThreeOfAKind
                    case 3: tempRanks.Add(HandRankEnum.ThreeOfAKind); break;

                    // is hand FourOfAKind
                    case 4: tempRanks.Add(HandRankEnum.FourOfAKind); break;

                    // is hand FiveOfAKind
                    case 5: tempRanks.Add(HandRankEnum.FiveOfAKind); break;
                }
            }


            // is hand FullHouse?
            if (tempRanks.Contains(HandRankEnum.ThreeOfAKind) && tempRanks.Count(rank => rank == HandRankEnum.OnePair) == 1)
            {
                tempRanks.Add(HandRankEnum.FullHouse);
            }

            // is hand Flush?
            foreach (int suit in Enum.GetValues(typeof(SuitEnum)))
            {
                bool isFlush = cards.All(card => card.Suit == (SuitEnum)suit);
                if (isFlush)
                {
                    tempRanks.Add(HandRankEnum.Flush);
                }
            }

            //TODO: figure out (Straight)


            //TODO: figure out (StraightFlush)


            // Default to HighCard if no rank was added
            HandRankEnum handRank = tempRanks.Any() ? tempRanks.Max() : HandRankEnum.HighCard;
            return handRank;
        }

        public List<FullCardInfo> GetListOfFullCardInfo(List<PlayingCard> cards)
        {
            //NOTE: Remember if hand is one pair then the count of cardCountNameValues will be 4 (won't change count depending on pairs)
            List<CardCountNameValue> cardCountNameValues = cards.GroupBy(card => card.Value)
                 .Select(group => new CardCountNameValue() { CardName = (CardNameValueEnum)group.Key, CardValue = group.Key, CardCount = group.Count() })
                 .ToList();

            //NOTE: Remember if hand is one pair then the count of fullCardInfos will be 5 (won't change count depending on pairs)
            List<FullCardInfo> fullCardInfos = cards.Select(card => new FullCardInfo()
            {
                Name = card.Name,
                Suit = card.Suit,
                CardCount = cardCountNameValues.First(c => c.CardName == card.Name).CardCount,
            }).ToList();

            return fullCardInfos;
        }
    }
}
