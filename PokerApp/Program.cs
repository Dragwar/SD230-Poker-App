using System;

namespace PokerApp
{
    public class Program
    {
        public static void Main()
        {
            Console.Title = $"Deck consists of: (52 total playing cards), (4 suits), (13 playing cards per suit)";

            (string userName, int? numberOfAI, bool? isStrictDraw) = SetUpGame();

            Start((userName, numberOfAI, isStrictDraw));
        }

        private static void Start((string userName, int? numberOfAI, bool? isStrictDraw)? data)
        {
            // if invalid data, then require data
            if (!data.HasValue ||
                string.IsNullOrWhiteSpace(data.Value.userName) ||
                !data.Value.isStrictDraw.HasValue ||
                !data.Value.numberOfAI.HasValue ||
                data.Value.numberOfAI < 1 ||
                data.Value.numberOfAI > 7)
            {
                data = SetUpGame();
            }

            IPlayer mainPlayer = new Player(data.Value.userName);
            GameManager game = new GameManager(mainPlayer, data.Value.numberOfAI.Value);


            Console.WriteLine("\nNow Dealing Five Cards To Each Player...");
            game.DealToEachPlayer();


            game.ModifyHands();


            Console.WriteLine("\nPlayer Hands:");
            game.DisplayHandInfo(true);
            game.CheckWinConditions(data.Value.isStrictDraw.Value);


            CheckToRestartGame((data.Value.userName, data.Value.numberOfAI.Value, data.Value.isStrictDraw));
        }

        private static (string, int?, bool?) SetUpGame()
        {
            // Player name
            string userName;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Please provide a name");
                userName = Console.ReadLine().Trim();
            } while (string.IsNullOrWhiteSpace(userName));


            // Number of players
            int num;
            int? numberOfAI;
            bool isNumber = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Number of AI players (between 1-7) [excluding yourself]");
                isNumber = int.TryParse(Console.ReadLine().Trim(), out num);
                numberOfAI = num;
            } while (!isNumber || numberOfAI.Value < 1 || numberOfAI.Value > 7);


            // Draw rules
            bool? isStrictDraw = null;
            ConsoleKey key;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Tie/Draw rules:");
                Console.WriteLine("[Strict-Draw] --> (Y) - Only players with the same Hand Rank and same HighCard Value will be a tie ");
                Console.WriteLine("[Non-Strict-Draw] --> (N) - Player's with the same hand will always be a tie/draw regardless of the HighCard");
                key = Console.ReadKey(true).Key;
                isStrictDraw = key == ConsoleKey.Y ? true : key == ConsoleKey.N ? false : isStrictDraw;
            } while ((key != ConsoleKey.Y || key != ConsoleKey.N) && !isStrictDraw.HasValue);

            return (userName, numberOfAI.Value, isStrictDraw.Value);
        }

        private static void CheckToRestartGame((string userName, int? numberOfAI, bool? isStrictDraw) gameData)
        {
            bool isValidKey = false;
            do
            {
                Console.WriteLine("\nPress 1 to Restart Test (with same settings)");
                Console.WriteLine("Press 2 to Restart Test (Full Reset)");
                Console.WriteLine("Press 3 to Exit");
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        isValidKey = true;
                        Console.Clear();
                        Console.WriteLine("Restarting . . .\n");
                        Start(gameData);
                        break;

                    case ConsoleKey.D2:
                        isValidKey = true;
                        Start(null);
                        break;

                    case ConsoleKey.D3: isValidKey = true; break;

                    default: break;
                }
            } while (!isValidKey);
        }
    }
}
