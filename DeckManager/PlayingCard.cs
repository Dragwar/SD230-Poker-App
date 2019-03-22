namespace PlayingCardsDeck
{
    /// <summary>
    ///     Represents a playing card - Example:
    ///     <para/>"Ten Spades" (<see cref="Value"/>: int 10, <see cref="Name"/>: <see cref="CardNameValueEnum"/> Ten, <see cref="Suit"/>: <see cref="SuitEnum"/> Spades)
    /// </summary>
    public class PlayingCard
    {
        /// <summary>
        ///     Card Name
        ///     <para/>Type - <see cref="CardNameValueEnum"/>
        /// </summary>
        public CardNameValueEnum Name { get; }

        /// <summary>
        ///     Card Suit
        ///     <para/>Type - <see cref="SuitEnum"/>
        /// </summary>
        public SuitEnum Suit { get; }

        /// <summary>
        ///     Card Value (not settable, uses <see cref="CardNameValueEnum"/> to Get Value)
        ///     <para/>Type - int (gets from (int)<seealso cref="Name"/>)
        /// </summary>
        public int Value { get => (int)Name; }

        public PlayingCard(CardNameValueEnum name, SuitEnum suit)
        {
            Name = name;
            Suit = suit;
        }

        public override string ToString() => $"{Name} - {Suit} - {Value}";
    }
}
