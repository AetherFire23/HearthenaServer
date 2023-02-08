using HearthenaServer.Entities;

namespace HearthenaServer.Interfaces
{
    public interface IPlayerRepository
    {
        public Task<Player> GetPlayerById(Guid id);
        public Task<Game> GetGameById(Guid gameId);


    }
}
