using System;

namespace PokerApp
{
    public class Program
    {
        public static void Main()
        {
            Console.Title = $"Deck consists of: (52 total playing cards), (4 suits), (13 playing cards per suit)";

            (string userName, int? numberOfAI, bool? isStrictDraw, bool? allowHandModify) = SetUpGame();

            Start((userName, numberOfAI, isStrictDraw, allowHandModify));
        }

        private static void Start((string userName, int? numberOfAI, bool? isStrictDraw, bool? allowHandModify)? data)
        {
            // if invalid data, then reacquire data
            if (!data.HasValue ||
                string.IsNullOrWhiteSpace(data.Value.userName) ||
                !data.Value.isStrictDraw.HasValue ||
                !data.Value.numberOfAI.HasValue ||
                !data.Value.allowHandModify.HasValue ||
                data.Value.numberOfAI < 1 ||
                data.Value.numberOfAI > 7)
            {
                data = SetUpGame();
            }

            IPlayer mainPlayer = new Player(data.Value.userName);
            GameManager game = new GameManager(mainPlayer, data.Value.numberOfAI.Value);


            Console.WriteLine("\nNow Dealing Five Cards To Each Player...");
            game.DealToEachPlayer();

            if (data.Value.allowHandModify.HasValue && data.Value.allowHandModify.Value)
            {
                game.ModifyHands();
            }


            Console.WriteLine("\nPlayer Hands:");
            game.DisplayHandInfo(true);
            game.CheckWinConditions(data.Value.isStrictDraw.Value);


            CheckToRestartGame((data.Value.userName, data.Value.numberOfAI.Value, data.Value.isStrictDraw, data.Value.allowHandModify));
        }

        private static (string, int?, bool?, bool?) SetUpGame()
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
            ConsoleKey tieDrawAnswer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Tie/Draw rules:");
                Console.WriteLine("[Strict-Draw] --> (Y) - Only players with the same Hand Rank and same HighCard Value will be a tie ");
                Console.WriteLine("[Non-Strict-Draw] --> (N) - Player's with the same hand will always be a tie/draw regardless of the HighCard");
                tieDrawAnswer = Console.ReadKey(true).Key;
                isStrictDraw = tieDrawAnswer == ConsoleKey.Y ? true : tieDrawAnswer == ConsoleKey.N ? false : isStrictDraw;
            } while ((tieDrawAnswer != ConsoleKey.Y || tieDrawAnswer != ConsoleKey.N) && !isStrictDraw.HasValue);


            // Modify hands?
            bool? allowModify = null;
            ConsoleKey modifyAnswer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Modify player hands?");
                Console.WriteLine("(Y) - Choose what player hands to modify");
                Console.WriteLine("(N) - Let system randomize hands and prevent modification");
                modifyAnswer = Console.ReadKey(true).Key;
                allowModify = modifyAnswer == ConsoleKey.Y ? true : modifyAnswer == ConsoleKey.N ? false : allowModify;
            } while ((modifyAnswer != ConsoleKey.Y || modifyAnswer != ConsoleKey.N) && !allowModify.HasValue);

            return (userName, numberOfAI.Value, isStrictDraw.Value, allowModify.Value);
        }

        private static void CheckToRestartGame((string userName, int? numberOfAI, bool? isStrictDraw, bool? allowHandModify) gameData)
        {
            ConsoleKey key;
            do
            {
                Console.WriteLine("\nPress 1 to Restart Test (With Same Settings)");
                Console.WriteLine("Press 2 to Restart Test (Full Reset)");
                Console.WriteLine("Press 3 to Exit");
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Console.WriteLine("Restarting . . .\n");
                        Start(gameData);
                        break;

                    case ConsoleKey.D2: Start(null); break;

                    case ConsoleKey.D3: Environment.Exit(0); break;

                    default: break;
                }
            } while (key != ConsoleKey.D3);
        }
    }
}
