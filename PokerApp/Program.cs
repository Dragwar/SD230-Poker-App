using System;

namespace PokerApp
{
    public class Program
    {
        public static void Main()
        {
            //TODO: re-factor this method by splitting it into multiple methods
            string userName;
            do
            {
                Console.WriteLine("Please provide a name");
                userName = Console.ReadLine().Trim();
            } while (string.IsNullOrWhiteSpace(userName));



        GameStart:
            IPlayer mainPlayer = new Player(userName);
            GameManager game = new GameManager(mainPlayer, 2);

            Console.Title = $"Deck consists of: (52 total playing cards), (4 suits), (13 playing cards per suit)";


            Console.WriteLine("Now Dealing Five Cards To Each Player...");
            game.DealToEachPlayer();


            //game.DisplayPlayerHand(mainPlayer, true);
            //game.ModifyHand(mainPlayer);


            Console.WriteLine("\nPlayer Hands:");
            game.DisplayHandInfo(true);
            game.CheckWinConditions(true);



        LeavingMenu:
            Console.WriteLine("\nPress 1 to Restart Test");
            Console.WriteLine("Press 2 to Exit");
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Console.WriteLine("Restarting . . .\n");
                    goto GameStart;

                case ConsoleKey.D2:
                    break;

                default:
                    goto LeavingMenu;
            }
        }
    }
}
