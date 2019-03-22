using System;
using System.Collections.Generic;
using System.Text;
using PlayingCardsDeck;

namespace PokerApp
{
    internal class Player : IPlayer
    {
        public string Name { get; set; }
        public List<PlayingCard> Hand { get; set; }

        public Player(string name)
        {
            Name = name;
            Hand = new List<PlayingCard>(5);
        }


    }
}
