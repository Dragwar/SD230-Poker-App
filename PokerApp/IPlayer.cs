using UserLibrary;

namespace PokerApp
{
    internal interface IPlayer : IUser
    {
        Hand Hand { get; set; }
        string NameAndHandRank { get; }
    }
}
