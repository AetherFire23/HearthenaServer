using HearthenaServer.Entities;

namespace HearthenaServer.Models
{
    public class BoardHelper
    {
        public List<Minion> Minions;
        public bool IsFull => Minions.Count == 7;
        public bool IsEmpty => Minions.Count == 0;


        public BoardHelper(List<Minion> minions)
        {
            var ordered = minions.OrderBy(x => x.BoardIndex).ToList();
            Minions = ordered;
        }

        public void InsertMinionInBoardSpace(Minion minion, int index) // could be other players minion thats why
        {
            // Warning : Maximum one minion over to add to the end of the list. so if only 1 minion on the board, maximum index is 1
            Minions.Insert(index, minion);
        }


        public Minion GetLeftMinionOrDefault(Minion minion)
        {
            int minionIndex = Minions.IndexOf(minion);

            if (minionIndex == 0) return null;


            var leftMinion = Minions[minionIndex - 1];
            return leftMinion;
        }

        public Minion GetRightMinionOrDefault(Minion minion)
        {
            int minionIndex = Minions.IndexOf(minion);

            if (minionIndex == Minions.Count - 1) return null;

            var rightMinion = Minions[minionIndex + 1];
            return rightMinion;
        }

    }
}
