using PlayingCardsDeck;
using System;
using System.Collections.Generic;

namespace PokerApp
{
    public class Program
    {
        public static void Main()
        {
            Console.Title = "Deck consists of: (52 total playing cards), (4 suits), (13 playing cards per suit)";

        Start:
            GameManager game = new GameManager(players: (new List<IPlayer>() { new Player("Everett"), new Player("Dan") }));
            DeckManager deckManager = new DeckManager();



            // Tests go here for now
            Console.WriteLine("Now Dealing Five Cards To Each Player...");
            foreach (IPlayer player in game.Players)
            {
                PlayingCard dealtCard = game.DeckManager.Deal();
                player.Hand.Cards.Add(dealtCard);
                Console.WriteLine($"{player.Name} got ({dealtCard.ToString()})");
            }

            foreach (IPlayer player in game.Players)
            {
                Console.WriteLine($"Player: {player.Name}");
                foreach (PlayingCard card in player.Hand.Cards)
                {
                    Console.WriteLine(card.ToString());
                }
            }


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
