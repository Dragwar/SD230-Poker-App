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

        // Only for development testing
        [Obsolete("This method is only for development testing")]
        internal void DisplayAllPlayersHands() => _Players.ForEach(player =>
        {
            Console.WriteLine($"\n{player.Name}'s Hand:"); player.Hand.Cards.ForEach(card => Console.WriteLine(card.ToString()));
        });

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


        internal void CheckWinConditions(IPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
