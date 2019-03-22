using PlayingCardsDeck;
using System;
using System.Collections.Generic;
using System.Text;
using UserLibrary;

namespace PokerApp
{
    internal interface IPlayer : IUser
    {
        Hand Hand { get; set; }
    }
}
