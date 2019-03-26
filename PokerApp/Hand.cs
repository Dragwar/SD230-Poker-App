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

        //TODO: Make rank checks more modular or separate them into methods
        private HandRankEnum DetermineRank()
        {
            // just for testing hands
            List<PlayingCard> testingHand = new List<PlayingCard>()
            {
                new PlayingCard(CardNameValueEnum.Ace, SuitEnum.Diamonds),
                new PlayingCard(CardNameValueEnum.Two, SuitEnum.Diamonds),
                new PlayingCard(CardNameValueEnum.Three, SuitEnum.Diamonds),
                new PlayingCard(CardNameValueEnum.Four, SuitEnum.Diamonds),
                new PlayingCard(CardNameValueEnum.Five, SuitEnum.Diamonds),
            };

            //List<FullCardInfo> cards = GetListOfFullCardInfo(testingHand);
            List<FullCardInfo> cards = GetListOfFullCardInfo(Cards);

            // all ranks the current hand falls under
            List<HandRankEnum> tempRanks = new List<HandRankEnum>();

            // (int Card Count, FullCardInfo Card)
            List<IGrouping<int, FullCardInfo>> cardGroupPairs = cards.GroupBy(card => card.CardCount).ToList();


            // Find pairs and {number}OfKinds
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

                    // does hand contain a ThreeOfAKind
                    case 3: tempRanks.Add(HandRankEnum.ThreeOfAKind); break;

                    // does hand contain a FourOfAKind
                    case 4: tempRanks.Add(HandRankEnum.FourOfAKind); break;

                    // is hand FiveOfAKind (skip checking anything else because this is the highest rank)
                    case 5: tempRanks.Add(HandRankEnum.FiveOfAKind); return HandRankEnum.FiveOfAKind;
                }
            }


            // skip these checks if hand is already a FourOfAKind (skip unnecessary rank checks)
            if (!tempRanks.Contains(HandRankEnum.FourOfAKind))
            {
                // is hand FullHouse?
                if (tempRanks.Contains(HandRankEnum.ThreeOfAKind) && tempRanks.Count(rank => rank == HandRankEnum.OnePair) == 1)
                {
                    tempRanks.Add(HandRankEnum.FullHouse);
                }

                // skip these checks if hand is already a FullHouse (skip unnecessary rank checks)
                if (!tempRanks.Contains(HandRankEnum.FullHouse))
                {
                    // is hand Flush?
                    foreach (int suit in Enum.GetValues(typeof(SuitEnum)))
                    {
                        bool isFlush = cards.All(card => card.Suit == (SuitEnum)suit);
                        if (isFlush)
                        {
                            tempRanks.Add(HandRankEnum.Flush);
                        }
                    }


                    // is hand Straight?
                    bool isStraight = true;
                    int minValue = cards.Min(card => card.Value);
                    for (int i = 0; i < 5; i++)
                    {
                        //re-factor (maybe): minValue++ could be moved below this if for more readability
                        if (cards[i].Value != minValue++)
                        {
                            isStraight = false;
                            break;
                        }
                    }

                    if (isStraight)
                    {
                        tempRanks.Add(HandRankEnum.Straight);
                    }
                }
            }


            // is hand a StraightFlush?
            if (tempRanks.Contains(HandRankEnum.Straight) && tempRanks.Contains(HandRankEnum.Flush))
            {
                tempRanks.Add(HandRankEnum.StraightFlush);
            }

            // Default to HighCard if no rank was added
            HandRankEnum handRank = tempRanks.Any() ? tempRanks.Max() : HandRankEnum.HighCard;

            return handRank;
        }

        internal List<FullCardInfo> GetListOfFullCardInfo(List<PlayingCard> cards)
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
