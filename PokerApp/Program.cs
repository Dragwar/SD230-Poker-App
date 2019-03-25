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
            GameManager game = new GameManager(players: (new List<IPlayer>()
            {
                new Player("Everett"),
                new Player("Darius"),
                new Player("Susan"),
                new Player("Jill"),
                new Player("Toby"),
                new Player("John"),
                new Player("Gui"),
                new Player("Dan"),
            }));

            // Tests go here for now
            Console.WriteLine("Now Dealing Five Cards To Each Player...");
            game.DealToEachPlayer();
            //game.DisplayAllPlayersHands();

            var handInfo = game.Players[0].Hand.GetCardCountNameValueList(game.Players[0].Hand.Cards);

            
            foreach (var cardinfo in handInfo)
            {
                Console.WriteLine(cardinfo.ToString());
            }
            Console.WriteLine(game.Players[0].Hand.Rank);


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
