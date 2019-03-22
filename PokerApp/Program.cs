using PlayingCardsDeck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerApp
{
    public class Program
    {
        public static void Main()
        {
            Console.Title = "Deck consists of: (52 total playing cards), (4 suits), (13 playing cards per suit)";

        Start:
            DeckManager deckManager = new DeckManager();

            List<PlayingCard> hand = new List<PlayingCard>();

            // Tests go here for now
            Console.WriteLine();
            deckManager.DisplayDeckInfo();
            Console.WriteLine();

            Console.WriteLine("Now Removing One Card From The Top...");
            Console.WriteLine($"Removed: {deckManager.Deal().ToString()}");

            Console.WriteLine();
            deckManager.DisplayDeckInfo();
            Console.WriteLine();

            Console.WriteLine("Now Removing Five Cards From The Top...");
            for (int i = 0; i < 5; i++)
            {
                PlayingCard dealtCard = deckManager.Deal();
                hand.Add(dealtCard);
                Console.WriteLine($"Removed: {dealtCard.ToString()}");
            }

            Console.WriteLine();
            deckManager.DisplayDeckInfo();
            Console.WriteLine();

            Console.WriteLine("High Card");
            List<PlayingCard> tempHand = new List<PlayingCard>();
            tempHand = ((PlayingCard[])hand.ToArray().Clone()).ToList();
            tempHand = tempHand.OrderBy(o => o.Value).ToList();
            PlayingCard HighCard = tempHand.Last();
            Console.WriteLine(HighCard.ToString());


        LeavingMenu:

            Console.WriteLine("\nPress 1 to Restart Test");
            Console.WriteLine("Press 2 to Exit");
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Console.WriteLine("Restarting . . .\n");
                    goto Start;

                case ConsoleKey.D2:
                    break;

                default:
                    goto LeavingMenu;
            }
        }
    }
}
