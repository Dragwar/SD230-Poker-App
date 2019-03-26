using System.Collections.Generic;
using System.Linq;

namespace PokerApp
{
    internal class WinConditionChecker
    {
        internal IReadOnlyList<IPlayer> Players { get; }
        internal IReadOnlyList<HandRankEnum> PlayerHandRanks { get; }
        internal HandRankEnum HighestRank { get; }
        internal int HighestRankValue { get => (int)HighestRank; }

        internal WinConditionChecker(IReadOnlyList<IPlayer> players)
        {
            Players = players;
            PlayerHandRanks = players.Select(player => player.Hand.Rank).ToList().AsReadOnly();
            HighestRank = PlayerHandRanks.Max();
        }

        internal IReadOnlyList<IPlayer> DetermineWinners()
        {
            List<IPlayer> winners = new List<IPlayer>();
            foreach (IPlayer player in Players)
            {
                if (player.Hand.Rank == HighestRank)
                {
                    winners.Add(player);
                }
            }

            // Filter out winners by the player with the highest card value.
            // More then one player can win, they just have to have the same hand rank
            // and the same HighCardValue to tie against other winners
            if (winners.Count >= 2 && winners.Count <= Players.Count)
            {
                // declares winner(s) by the player with the maxHighCardValue
                // if more than one player has the same high card value to maxHighCardValue then it is a draw
                // else then the one player with the high card value that matches maxHighCardValue
                int maxHighCardValue = winners.Max(player => player.Hand.HighCard.Value);
                winners = winners.Where(player => player.Hand.HighCard.Value == maxHighCardValue).ToList();
                return winners.AsReadOnly();
            }
            else if (winners.Count == 1)
            {
                return winners.AsReadOnly();
            }
            else
            {
                throw new System.Exception("Something went wrong (winner calculation gone wrong)");
            }
        }
    }
}
