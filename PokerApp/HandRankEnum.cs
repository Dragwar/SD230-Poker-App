using System;
using System.Collections.Generic;
using System.Text;

namespace PokerApp
{
    /// <summary>
    ///     The int value represents the quality of hand
    ///     (0 is the lowest and 9 being the highest ranking hand)
    /// </summary>
    public enum HandRankEnum
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        FiveOfAKind,
    }
}
