using HearthenaServer.Entities;
using HearthenaServer.Interfaces;

namespace HearthenaServer.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly HearthenaContext _context;
        public PlayerRepository(HearthenaContext context)
        {
            _context = context;
        }
        public async Task<Player> GetPlayerById(Guid id)
        {
            var player = _context.Players.FirstOrDefault(p => p.Id == id);
            return player;
        }

        public async Task<Game> GetGameById(Guid gameId)
        {
            return _context.Games.FirstOrDefault(x => x.Id == gameId);
        }

        public async Task<Player> GetPlayingPlayer(Game game)
        {
            return game.Player1.IsPlaying
            ? game.Player1
            : game.Player2;
        }

        public async Task<Player> GetNonPlayingPlayer(Game game)
        {
            return !game.Player1.IsPlaying
            ? game.Player1
            : game.Player2;
        }
    }
}