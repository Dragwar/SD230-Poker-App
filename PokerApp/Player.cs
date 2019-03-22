using System;
using System.Collections.Generic;
using System.Text;
using PlayingCardsDeck;

namespace PokerApp
{
    internal class Player : IPlayer
    {
        public string Name { get; set; }
        public Hand Hand { get; set; }

        internal Player(string name)
        {
            Name = name;
            Hand = new Hand(new List<PlayingCard>());
        }


    }
}
