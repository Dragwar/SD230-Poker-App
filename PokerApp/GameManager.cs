using PlayingCardsDeck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerApp
{
    internal class GameManager
    {
        private Random Random { get; }
        private List<IPlayer> _Players { get; set; }
        private DeckManager DeckManager { get; }

        private const int PlayerHandSize = 5;
        private const int MaxPlayerLimit = 8;
        private const int MinPlayerLimit = 2;

        internal IReadOnlyList<IPlayer> Players { get => _Players.ToList().AsReadOnly(); }
        internal IReadOnlyList<PlayingCard> Deck { get => DeckManager.Deck.AsReadOnly(); }


        internal GameManager(List<IPlayer> players)
        {
            Random = new Random();
            _Players = CheckValidPlayerCount(players);
            DeckManager = new DeckManager(Random.Next(4, 10));
        }
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


        internal void DisplayAllPlayersHands(bool displayColorCodedSuits) => _Players.ForEach(player =>
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($" {player.Name}'s Hand: ({player.Hand.Rank})");
            Console.ResetColor();

            List<FullCardInfo> handInfo = player.Hand.GetListOfFullCardInfo(player.Hand.Cards);

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
        });

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


        internal void CheckWinConditions()
        {
            WinConditionChecker checker = new WinConditionChecker(Players);

            IReadOnlyList<IPlayer> winners = checker.DetermineWinner();
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
