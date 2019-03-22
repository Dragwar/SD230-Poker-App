using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace PlayingCardsDeck
{
    public static class ShuffleHelper
    {
        /// <summary>
        ///     Provides random numbers and
        ///     this is a property because Random
        ///     produces random numbers based on the
        ///     seed (seed is made by the current DateTime)
        ///     <para/>(in other words this is just for randomness consistency)
        /// </summary>
        private static Random Random { get; } = new Random();

        /// <summary>
        ///     <para/>I'm trying out recursion to shuffle the <paramref name="list"/> multiple times
        ///     <para/>This method will default to 5 passes (<paramref name="list"/> gets shuffled 5 times)
        /// </summary>
        /// <param name="list">
        ///     Represents the list that will be shuffled
        /// </param>
        /// <param name="numberOfPassesLimit">
        ///     Determines many times the <paramref name="list"/> will be shuffled
        /// </param>
        /// <param name="numberOfPassesCounter">
        ///     Counts the number of times this function gets invoked (similar to i in for...loops)
        /// </param>
        /// <returns>
        ///     Shuffled int <paramref name="list"/>
        /// </returns>
        private static List<T> ShuffleLoop<T>(this List<T> list, int? numberOfPassesLimit, int numberOfPassesCounter = 0)
        {
            if (numberOfPassesCounter >= numberOfPassesLimit.Value)
            {
                return list;
            }

            // .OrderBy() will output something similar to .Sort() when:
            // Sort: ((new int[] {3,2,4}).Sort() -> {2,3,4})
            // OrderBy: ((new int[] {3,2,4}).OrderBy(n => n) -> {2,3,4})
            //
            // but .OrderBy() operates on element and key to sort (sorts by key value)
            // .OrderBy(n /* n is the current element of the current iteration */ => /* This is where you define a key to sort by */)
            // since I'm using Random.Next(), then .OrderBy() tries to sort by a random int key
            // (each iteration Random.Next() provides a new random int for the key)
            // in other words the currentNumber value is ignored when it comes to OrderBy and
            // the Random.Next() int key will be use to sort the array
            //
            // NOTE: You can also use Guid.NewGuid() for the key to sort as well as Random.Next(),
            // and I'm guessing Guid.NewGuid() would be a better option then Random.Next()
            return list.OrderBy(currentNumber => Random.Next()).ToList().ShuffleLoop(numberOfPassesLimit.Value, numberOfPassesCounter + 1);
        }

        /// <summary>
        ///     Only using this to prevent user/developer from
        ///     changing <paramref name="numberOfPassesLimit"/> parameter
        /// </summary>
        /// <param name="list">
        ///     Represents the <paramref name="list"/> that will be shuffled
        /// </param>
        /// <param name="numberOfPassesLimit">
        ///     Determines many times the <paramref name="list"/> will be shuffled
        /// </param>
        /// <returns>
        ///     Fully shuffled int <paramref name="list"/>
        /// </returns>
        public static List<T> Shuffle<T>(this List<T> list, [Optional] int? numberOfPassesLimit)
        {
            // default to 5 if number is below 0, else if number is greater then 50 then default to 50
            if (numberOfPassesLimit.HasValue)
            {
                numberOfPassesLimit = numberOfPassesLimit < 0 ? 5 : numberOfPassesLimit > 50 ? 50 : numberOfPassesLimit;
            }
            else
            {
                numberOfPassesLimit = 5;
            }
            return list.ShuffleLoop(numberOfPassesLimit);
        }


        /// <summary>
        ///     <para/>Got this function from my Old JavaScript SD110 Final Project (memory card game)
        ///     <para/>I only added on a numberOfPasses functionality recently
        /// </summary>
        /// <typeparam name="T">Represents the type of element in <paramref name="list"/></typeparam>
        /// <param name="list">Represents the <paramref name="list"/> that will used to be shuffled</param>
        /// <param name="numberOfPasses">Determines many times the <paramref name="list"/> will be shuffled</param>
        /// <returns>Returns a shuffled List</returns>
        [Obsolete]
        public static List<T> OldShuffle<T>(this List<T> list, int numberOfPasses = 1)
        {
            int currentIndex = list.Count, randomIndex, counter = 0;
            T temporaryValue;
            do
            {
                while (currentIndex != 0)
                {
                    randomIndex = Random.Next(0, list.Count);
                    currentIndex -= 1;
                    temporaryValue = list[currentIndex];
                    list[currentIndex] = list[randomIndex];
                    list[randomIndex] = temporaryValue;
                }
                counter++;
            } while (counter < numberOfPasses);

            return list;
        }


        /// <summary>
        ///     Simple Method to display an <paramref name="list"/> of <typeparamref name="T"/>
        ///     on one line in the console
        /// </summary>
        /// <param name="list">
        ///     <paramref name="list"/> that will have its elements
        ///     displayed in the console
        /// </param>
        /// <param name="isWriteLine">
        ///     Determines whether or not to display elements in one line
        ///     or to print a new line for each element
        /// </param>
        /// <returns>
        ///     Returns passed in <paramref name="list"/>
        /// </returns>
        public static List<T> Display<T>(this List<T> list, bool isWriteLine = false)
        {
            if (isWriteLine)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine(list[i]);
                }
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if ((i + 1) == list.Count)
                    {
                        Console.Write("{0}\n\n", list[i]);
                    }
                    else
                    {
                        Console.Write("{0}, ", list[i]);
                    }
                }
            }
            return list;
        }
    }
}
