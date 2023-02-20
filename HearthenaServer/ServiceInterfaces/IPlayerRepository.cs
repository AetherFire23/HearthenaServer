using HearthenaServer.DTO;
using HearthenaServer.Entities;

namespace HearthenaServer.Interfaces
{
    public interface IPlayerRepository
    {
        public Task<Player> GetPlayerById(Guid id);
        public Task<Game> GetGameById(Guid gameId);
        public Task<Player> GetPlayingPlayer(Game game);
        public Task<Player> GetNonPlayingPlayer(Game game);
        public Task<ICharacter> GetTarget(Dictionary<string, string> targetParameters);
        public Task<HeroDTO> CreateHeroDTO(Guid heroId);
        public Task<List<Card>> GetCardsInHand(Guid playerId);
        public Task<List<Card>> GetCardsInDeck(Guid playerId);
        public Task<Game> GetGameByPlayerId(Guid playerId);
        public Task<List<Card>> GetBlankCards(List<Card> cards);
        public Task<ICharacter> GetTargetById(Guid targetId);
    }
}
