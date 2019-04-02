namespace PokerApp
{
    internal interface IPlayer
    {
        string Name { get; }
        Hand Hand { get; set; }
        void ModifyHand();
    }
}
