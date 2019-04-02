using PlayingCardsDeck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace PokerApp
{
    internal class GameManager
    {
        internal List<IPlayer> PremadePlayers => new List<IPlayer>()
        {
            new Player("Darius"),
            new Player("Susan"),
            new Player("Jill"),
            new Player("Toby"),
            new Player("Dan"),
            new Player("Colton"),
            new Player("Aris"),
            new Player("Max"),
            new Player("Amy"),
            new Player("Cory"),
            new Player("Chloe"),
            new Player("Kim"),
            new Player("Bob"),
            new Player("Bryan"),
            new Player("Miguel"),
            new Player("Lee"),
            new Player("Eddie"),
            new Player("Steve"),
        };

        private Random Random { get; }
        private List<IPlayer> _Players { get; set; }
        private DeckManager DeckManager { get; }

        private const int PlayerHandSize = 5;
        private const int MaxPlayerLimit = 8;
        private const int MinPlayerLimit = 2;

        internal IReadOnlyList<IPlayer> Players { get => _Players.ToList().AsReadOnly(); }
        internal IReadOnlyList<PlayingCard> Deck { get => DeckManager.Deck.AsReadOnly(); }

        internal static List<CardNameValueEnum> GetCardNameValuesEnums() => Enum.GetValues(typeof(CardNameValueEnum))
           .Cast<CardNameValueEnum>()
           .ToList();

        internal static List<SuitEnum> GetCardSuitEnum() => Enum.GetValues(typeof(SuitEnum))
                .Cast<SuitEnum>()
                .ToList();

        /// <param name="players">Count has to be within (2 - 8)</param>
        internal GameManager(List<IPlayer> players)
        {
            Random = new Random();
            _Players = CheckValidPlayerCount(players);
            DeckManager = new DeckManager(Random.Next(4, 10));
        }

        /// <param name="mainPlayer">This is the Main Player of the game</param>
        /// <param name="amountOfAIPlayers">
        ///     Range has to be within (1 - 7)
        ///     If left null it will default to a int between (1 - 7)
        /// </param>
        internal GameManager(IPlayer mainPlayer, [Optional] int? amountOfAIPlayers)
        {
            Random = new Random();
            amountOfAIPlayers = DetermineAmountOfPlayers(amountOfAIPlayers);
            _Players = PremadePlayers.Shuffle()
                .Take(amountOfAIPlayers.Value)
                .Append(mainPlayer)
                .ToList();

            if (_Players.Count > MaxPlayerLimit || _Players.Count < MinPlayerLimit)
            {
                throw new Exception("invalid amount of players");
            }

            DeckManager = new DeckManager(Random.Next(4, 10));
        }
        #region RangeChecks
        private List<IPlayer> CheckValidPlayerCount(List<IPlayer> players)
        {
            if (players.Count < MinPlayerLimit)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (players.Count > MaxPlayerLimit)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                return players;
            }
        }
        private int DetermineAmountOfPlayers(int? amountOfPlayers)
        {
            if (!amountOfPlayers.HasValue)
            {
                return Random.Next(MinPlayerLimit, MaxPlayerLimit);
            }
            else if (amountOfPlayers.Value < MinPlayerLimit - 1 || amountOfPlayers.Value > MaxPlayerLimit - 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (amountOfPlayers.Value >= MinPlayerLimit - 1 || amountOfPlayers.Value <= MaxPlayerLimit - 1)
            {
                return amountOfPlayers.Value;
            }
            else
            {
                throw new Exception("Something went wrong!");
            }
        }
        #endregion


        /// <param name="player">If this parameter is null then you will be prompted to change all player hands</param>
        internal void ModifyHands(IPlayer player = null)
        {
            if (player == null)
            {
                foreach (IPlayer currentPlayer in _Players)
                {
                    ConsoleKey key;
                    do
                    {
                        DisplayPlayerHand(currentPlayer, true);
                        Console.WriteLine($"Modify {currentPlayer.Name}'s hand? (y or n)\n");
                        key = Console.ReadKey(true).Key;

                        switch (key)
                        {
                            case ConsoleKey.Y: (currentPlayer as Player).ModifyHand(); break;
                            case ConsoleKey.N: break;
                        }
                        if (key == ConsoleKey.N)
                        {
                            break;
                        }
                    } while (key != ConsoleKey.Y);
                }
            }
            else
            {
                ConsoleKey key;
                do
                {
                    DisplayPlayerHand(player, true);
                    Console.WriteLine($"Modify {player.Name}'s hand? (y or n)\n");
                    key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.Y: (player as Player).ModifyHand(); break;
                        case ConsoleKey.N: break;
                    }
                } while (key != ConsoleKey.Y || key != ConsoleKey.N);
            }

            Console.Clear();
        }


        private void DisplayPlayerHand(IPlayer player, bool displayColorCodedSuits)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($" {player.Name}'s Hand: ({player.Hand.Rank})");
            Console.ResetColor();

            List<FullCardInfo> handInfo = player.Hand.GetListOfFullCardInfo(player.Hand.Cards)
                .OrderBy(card => card.Value)
                .ToList();

            foreach (FullCardInfo cardinfo in handInfo)
            {
                if (displayColorCodedSuits)
                {
                    switch (cardinfo.Suit)
                    {
                        case SuitEnum.Clubs: Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                        case SuitEnum.Diamonds: Console.ForegroundColor = ConsoleColor.DarkRed; break;
                        case SuitEnum.Hearts: Console.ForegroundColor = ConsoleColor.Red; break;
                        case SuitEnum.Spades: Console.ForegroundColor = ConsoleColor.Blue; break;
                        default: Console.ForegroundColor = ConsoleColor.Gray; break;
                    }
                }
                Console.WriteLine("  - " + cardinfo.ToString());
                Console.ResetColor();
            }
        }

        internal void DisplayHandInfo(bool displayColorCodedSuits, [Optional] IPlayer mainPlayer)
        {
            if (mainPlayer == null)
            {
                foreach (IPlayer player in Players)
                {
                    DisplayPlayerHand(player, displayColorCodedSuits);
                }
            }
            else
            {
                DisplayPlayerHand(mainPlayer, displayColorCodedSuits);
            }
        }

        internal void DealToEachPlayer()
        {
            foreach (IPlayer player in _Players)
            {
                for (int i = 0; i < PlayerHandSize; i++)
                {
                    PlayingCard dealtCard = DeckManager.Deal();
                    player.Hand.Cards.Add(dealtCard);
                }
            }
        }


        internal void CheckWinConditions(bool filterWinnersOnDrawsByHighCardValue)
        {
            WinConditionChecker checker = new WinConditionChecker(Players, filterWinnersOnDrawsByHighCardValue);

            IReadOnlyList<IPlayer> winners = checker.DetermineWinners();
            Console.WriteLine("\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (IPlayer player in winners)
            {
                if (winners.Count > 1)
                {
                    if (player == winners.First())
                    {
                        Console.Write($" Draw Between: {player.Name}, ");
                    }
                    else if (player == winners.Last())
                    {
                        Console.Write($"{player.Name} ({checker.HighestRank})\n\n");
                    }
                    else
                    {
                        Console.Write($"{player.Name}, ");
                    }
                }
                else
                {
                    Console.WriteLine($" {player.Name} has Won! ({checker.HighestRank})\n");
                }
            }
            Console.ResetColor();
        }
    }
}
