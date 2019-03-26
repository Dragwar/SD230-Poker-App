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

        internal IReadOnlyList<IPlayer> DetermineWinner()
        {
            List<IPlayer> winners = new List<IPlayer>();
            foreach (IPlayer player in Players)
            {
                if (player.Hand.Rank == HighestRank)
                {
                    winners.Add(player);
                }
            }

            //TODO: filter out winners by HighCardValue (get the winner list to be as short as possible)
            /*
            if (winners.Count > 1)
            {
                winners = winners.Where(player => player.Hand.HighCard.Value == winners.Max(p => p.Hand.HighCard.Value)).ToList();
                return winners;
            }
            else if (winners.Count > 1)
            {
                return winners;
            }
            else
            {
                throw new System.Exception("Something went wrong (winner calculation gone wrong)");
            }
            */
            return winners.AsReadOnly();
        }
    }
}
