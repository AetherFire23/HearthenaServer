﻿using HearthenaServer.DTO;
using HearthenaServer.Entities;

namespace HearthenaServer.Interfaces
{
    public interface IPlayerRepository
    {
        public Task<Player> GetPlayerById(Guid id);
        public Task<Game> GetGameById(Guid gameId);
        public Task<Player> GetPlayingPlayer(Game game);
        public Task<Player> GetNonPlayingPlayer(Game game);
        public Task<List<Card>> GetCardsInHand(Player player);
        public Task<List<Card>> GetCardsInDeck(Player player);
        public Task<ICharacter> GetTarget(Guid targetId, Type targetType);
        public Task<ICharacter> GetTarget(Dictionary<string, string> targetParameters);
        public Task<HeroDTO> CreateHeroDTO(Guid heroId);
    }
}