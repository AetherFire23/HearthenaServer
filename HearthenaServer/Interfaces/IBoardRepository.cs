using HearthenaServer.Entities;

namespace HearthenaServer.Interfaces
{
    public interface IBoardRepository
    {
        public Task UpdateMinionBoardIndexes(List<Minion> minions);

    }
}
