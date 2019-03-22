using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace PlayingCardsDeck
{
    /// <summary>
    ///     <para/>Holds Cards that can be dealt and shuffled,
    ///     <para/>Static SortedPlayingCards represents the Cards before it got shuffled
    ///     <para/>Classes Actions:
    ///     <para/>- Cards will be shuffled on Deck creation
    ///     <para/>- Cards can be shuffled after Deck creation
    ///     <para/>- Cards can be dealt one at a time
    ///     <para/>- Cards can be dealt all at once
    /// </summary>
    public class DeckManager
    {
        public DeckManager(int numberOfInitialShuffles = 5)
        {
            Deck = BuildDeck().Shuffle(numberOfInitialShuffles);
        }

        /// <summary>
        ///     Instead of using BuildDeck() to see the default deck you can use this property
        /// </summary>
        public static List<PlayingCard> SortedDeck { get; } = new List<PlayingCard>(BuildDeck());

        /// <summary>
        ///     Represents all the playing cards in the deck
        /// </summary>
        public List<PlayingCard> Deck { get; private set; }


        /// <summary>
        ///     Shuffles the cards
        /// </summary>
        /// <param name="numberOfShuffles">(Optional) Represents the number of times the deck will be shuffled</param>
        public void ShuffleDeck([Optional] int? numberOfShuffles)
        {
            Deck = Deck.Shuffle(numberOfShuffles);
        }


        /// <summary>
        ///     Resets Cards to initial state (52 shuffled Cards)
        /// </summary>
        /// <param name="numberOfShuffles">(Optional) Represents the number of times the deck will be shuffled</param>
        public void ResetToNewShuffledDeck([Optional] int? numberOfShuffles)
        {
            Deck = BuildDeck().Shuffle(numberOfShuffles);
        }


        /// <summary>
        ///     <para/>Deals one card from the top "Cards[0]" (unless optional parameter was used)
        ///     <para/>Before dealing the card, the card will be removed from Cards, then it will be dealt
        /// </summary>
        /// <param name="takeCardAt">(Optional) Deal card from a certain index</param>
        /// <returns>Returns the card that was dealt</returns>
        public PlayingCard Deal([Optional] int? takeCardAt)
        {
            if (Deck.Any())
            {
                takeCardAt = takeCardAt ?? 0;
                takeCardAt = takeCardAt > Deck.Count ? Deck.Count - 1 : takeCardAt;
                PlayingCard card = Deck[takeCardAt.Value];
                Deck.RemoveAt(takeCardAt.Value);
                return card;
            }
            else if (!Deck.Any())
            {
                Console.WriteLine("No cards were left to deal!");
                return null;
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }


        /// <summary>
        ///     <para/>Deals all cards if there are any left, then removes all cards from the Cards list.
        ///     <para/>Lastly, this method will return the cards that were removed
        /// </summary>
        /// <returns>Returns the current state of the Cards</returns>
        public List<PlayingCard> DealAll()
        {
            if (Deck.Any())
            {
                List<PlayingCard> cards = new List<PlayingCard>(Deck);
                Deck.Clear();
                return cards;
            }
            else if (!Deck.Any())
            {
                Console.WriteLine("No cards were left to deal!");
                return null;
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }


        /// <summary>
        ///     <para/>Used to get a sorted list of 52 playing cards
        ///     <para/>(4 suits and 13 cards in each suit) unless Suit/CardsEnum was changed
        /// </summary>
        /// <returns>Returns a sorted list of playing cards</returns>
        private static List<PlayingCard> BuildDeck()
        {
            Array suits = Enum.GetValues(typeof(SuitEnum));
            Array cards = Enum.GetValues(typeof(CardNameValueEnum));

            List<PlayingCard> playingCards = new List<PlayingCard>();

            foreach (int suit in suits)
            {
                foreach (int card in cards)
                {
                    playingCards.Add(new PlayingCard((CardNameValueEnum)card, (SuitEnum)suit));
                }
            }

            return playingCards;
        }


        public void DisplayDeckInfo()
        {
            Console.WriteLine($"Total Cards: {Deck.Count}");
            Console.WriteLine($"Total Spades: {Deck.Count(card => card.Suit == SuitEnum.Spades)}");
            Console.WriteLine($"Total Clubs: {Deck.Count(card => card.Suit == SuitEnum.Clubs)}");
            Console.WriteLine($"Total Diamonds: {Deck.Count(card => card.Suit == SuitEnum.Diamonds)}");
            Console.WriteLine($"Total Hearts: {Deck.Count(card => card.Suit == SuitEnum.Hearts)}");
        }
    }
}
