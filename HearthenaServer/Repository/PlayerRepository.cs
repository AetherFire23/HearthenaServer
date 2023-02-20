using AutoMapper;
using HearthenaServer.DTO;
using HearthenaServer.Entities;
using HearthenaServer.Extensions;
using HearthenaServer.Interfaces;
using HearthenaServer.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace HearthenaServer.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly HearthenaContext _context;
        private readonly IMapper _mapper;

        public PlayerRepository(HearthenaContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Player> GetPlayerById(Guid id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
            return player;
        }

        public async Task<ICharacter> GetTarget(Guid targetId, Type targetType)
        {
            ICharacter attackedTarget = targetType == typeof(Minion)
                ? await _context.Minions.FirstOrDefaultAsync(x => x.Id == targetId) as ICharacter
                : await _context.Heroes.FirstOrDefaultAsync(x => x.Id == targetId) as ICharacter;

            if (attackedTarget is null) throw new ArgumentNullException($"Either {targetId} or {targetType} was null.");

            return attackedTarget;
        }

        public async Task<GameState> GetGameState(Guid playerId)
        {
            var player = await GetPlayerById(playerId);
            var game = await GetGameById(player.GameId);

            GameState gameState = new GameState()
            {
                Player = player,
                Player2 = game.
            };


        }

        public async Task<ICharacter> GetTarget(Dictionary<string, string> targetParameters)
        {
            Guid targetId = targetParameters.GetTargetId();
            Type targetType = targetParameters.GetTargetType();
            return await this.GetTarget(targetId, targetType);
        }

        public async Task<Game> GetGameById(Guid gameId)
        {
            return await _context.Games.FirstOrDefaultAsync(x => x.Id == gameId);
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

        public async Task<List<Card>> GetCardsInHand(Player player)
        {
            var cardsInHand = await _context.Cards.Where(c => c.IsInHand && c.OwnerId == player.Id).ToListAsync();
            return cardsInHand;
        }
        public async Task<List<Card>> GetCardsInDeck(Player player)
        {
            var cardsInDeck = await _context.Cards.Where(c => c.IsInHand == false && c.OwnerId == player.Id).ToListAsync();
            return cardsInDeck;
        }

        public async Task<HeroDTO> CreateHeroDTO(Guid heroId)
        {
            var heroEntity = await _context.Heroes.FirstOrDefaultAsync(x => x.Id == heroId);
            var heroDto = _mapper.Map<HeroDTO>(heroEntity);

            heroDto.Weapon = await _context.Weapons.FirstOrDefaultAsync(x => x.HeroId == heroId);

            return heroDto;
        }
    }
}