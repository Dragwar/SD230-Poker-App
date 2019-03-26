using PlayingCardsDeck;
using System.Collections.Generic;

namespace PokerApp
{
    internal class Player : IPlayer
    {
        public string Name { get; set; }
        public Hand Hand { get; set; }
        public string NameAndHandRank { get => $"{Name} ({Hand.Rank})"; }

        internal Player(string name)
        {
            Name = name;
            Hand = new Hand(new List<PlayingCard>());
        }

        public override string ToString() => $"{Name}";
    }
}
