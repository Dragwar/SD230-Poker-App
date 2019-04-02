using PlayingCardsDeck;
using System;
using System.Collections.Generic;

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

        public void ModifyHand()
        {
            //WARNING: this list is off by one (e.g. ace == 0, so ace == 0 + 1)
            List<CardNameValueEnum> cardNameValues = GameManager.GetCardNameValuesEnums();

            List<SuitEnum> cardSuits = GameManager.GetCardSuitEnums();


            List<CardNameValueEnum> newCardValues = new List<CardNameValueEnum>();
            List<SuitEnum> newCardSuits = new List<SuitEnum>();
            List<PlayingCard> newCards = new List<PlayingCard>();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nCard Value: [1 - 13] (e.g. \"Ace\", \"1\" or \"Jack\", \"11\")");
            Console.ResetColor();

            for (int i = 0; i < 5; i++)
            {
                // get desired Card Value
                newCardValues.Add(GetNewCardNameValueEnum(cardNameValues));

                Console.WriteLine();

                // get desired Card Suit
                newCardSuits.Add(GetNewCardSuitEnum(cardSuits));

                // make new card from selected values above
                PlayingCard newPlayingCard = new PlayingCard(newCardValues[i], newCardSuits[i]);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nCard Added! ({newPlayingCard.ToString()})\n");
                Console.ResetColor();

                newCards.Add(newPlayingCard);
            }
            Hand.Cards = newCards;
        }

        private SuitEnum GetNewCardSuitEnum(List<SuitEnum> cardSuits)
        {
            bool isValidSuitNumber;
            int suitNumber;
            do
            {
                // decide card suit (clubs, 0)
                for (int i = 0; i < cardSuits.Count; i++)
                {
                    Console.WriteLine($"Press {i + 1} - {cardSuits[i]}");
                }

                isValidSuitNumber = int.TryParse(Console.ReadLine().Trim(), out suitNumber);
                suitNumber--;
            } while (!isValidSuitNumber || suitNumber < 0 || (suitNumber + 1) > cardSuits.Count);

            return (SuitEnum)suitNumber;
        }

        private CardNameValueEnum GetNewCardNameValueEnum(List<CardNameValueEnum> cardNameValues)
        {
            bool isValidValueNumber;
            int valueNumber;
            do
            {
                // decide card value (ace, 1)
                for (int j = 0; j < cardNameValues.Count; j++)
                {
                    Console.WriteLine($"Press {j + 1} - {cardNameValues[j]}");
                }

                isValidValueNumber = int.TryParse(Console.ReadLine().Trim(), out valueNumber);

            } while (!isValidValueNumber || valueNumber < 1 || valueNumber > cardNameValues.Count);

            return (CardNameValueEnum)valueNumber;
        }

        public override string ToString() => $"{Name}";
    }
}


//private delegate bool ValidRange(int number);
/// <typeparam name="T">only allow enum types</typeparam>
/// <param name="listEnum"></param>
/// <param name="validRange"></param>
/// <param name="offsetBeforeCheck"></param>
/// <returns>The selected enum</returns>
//private T GetNewCardSuitEnum<T>(List<T> listEnum, ValidRange validRange, int offsetBeforeCheck = 0) where T : struct, IConvertible
//{
//    if (!typeof(T).IsEnum)
//    {
//        throw new ArgumentException("T must be an enum type");
//    }

//    bool isValidSuitNumber;
//    int suitNumber;
//    do
//    {
//        // decide card suit (clubs, 0)
//        for (int i = 0; i < listEnum.Count; i++)
//        {
//            Console.WriteLine($"Press {i + 1} - {listEnum[i]}");
//        }

//        isValidSuitNumber = int.TryParse(Console.ReadLine().Trim(), out suitNumber);
//        suitNumber -= offsetBeforeCheck;
//    } while (!isValidSuitNumber || !validRange(suitNumber));

//    try
//    {
//        // convert to passed in Enum
//        return (T)(suitNumber as object);
//    }
//    catch
//    {
//        throw new ArgumentException($"something went wrong with the generic you used");
//    }
//}
