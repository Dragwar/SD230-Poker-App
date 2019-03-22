using PlayingCardsDeck;
using System;
using System.Collections.Generic;

namespace PokerApp
{
    internal class GameManager
    {
        internal Random Random { get; set; }
        internal List<IPlayer> Players { get; private set; }
        internal DeckManager DeckManager { get; }

        public GameManager(List<IPlayer> players)
        {
            Random = new Random();
            Players = players;
            DeckManager = new DeckManager(Random.Next(4, 10));
        }



        internal void CheckWinConditions(IPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
