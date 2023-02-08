using HearthenaServer.Entities;
using HearthenaServer.Interfaces;

namespace HearthenaServer.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private readonly HearthenaContext _context;
        public BoardRepository(HearthenaContext context)
        {
            _context = context;
        }

        public async Task UpdateMinionBoardIndexes(List<Minion> minions)
        {
            for (int i = 0; i < minions.Count; i++)
            {
                var minion = minions[i];
                minion.BoardIndex = i;
            }

            await _context.SaveChangesAsync();
        }
    }
}
