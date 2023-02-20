using AutoMapper;
using HearthenaServer.DTO;
using HearthenaServer.Entities;
using HearthenaServer.Extensions;
using HearthenaServer.Interfaces;
using HearthenaServer.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Shared_Models.Constants;
using Shared_Models.DTO;
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

        public async Task<Player> GetOppositePlayer(Guid playerId)
        {
            var game = await this.GetGameByPlayerId(playerId);
            var opponent = game.Player1Id == playerId
                ? game.Player2
                : game.Player1;
            return opponent;
        }

        public async Task<Game> GetGameByPlayerId(Guid playerId)
        {
            var game = await _context.Games.FirstOrDefaultAsync(x =>
                x.Player1Id == playerId
                || x.Player2Id == playerId);

            if (game is null) throw new ArgumentNullException("Game was null");

            return game;
        }

        public async Task<ICharacter> GetTargetById(Guid targetId)
        {
            return null;
        }

        public async Task<GameState> GetGameState(Guid localPlayerId)
        {
            var localPlayerDTO = await this.CreateLocalPlayerDTO(localPlayerId);
            var opponentDTO = await this.CreateOpponentPlayerDTO(localPlayerId);
            GameState gameState = new GameState()
            {
                LocalPlayer = localPlayerDTO,
                Opponent = opponentDTO,

            };

            return gameState;
        }

        public async Task<ICharacter> GetTarget(Dictionary<string, string> targetParameters)
        {
            Guid targetId = targetParameters.GetTargetId();
            Type targetType = targetParameters.GetTargetType();
            return await this.GetTargetById(targetId);
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

        public async Task<List<Card>> GetCardsInHand(Guid playerId)
        {
            var cardsInHand = await _context.Cards.Where(c => c.IsInHand && c.OwnerId == playerId).ToListAsync();
            return cardsInHand;
        }
        public async Task<List<Card>> GetCardsInDeck(Guid playerId)
        {
            var cardsInDeck = await _context.Cards.Where(c => c.IsInHand == false && c.OwnerId == playerId).ToListAsync();
            return cardsInDeck;
        }

        public async Task<HeroDTO> CreateHeroDTO(Guid heroId)
        {
            var heroEntity = await _context.Heroes.FirstOrDefaultAsync(x => x.Id == heroId);
            var heroDto = _mapper.Map<HeroDTO>(heroEntity);

            heroDto.Weapon = await _context.Weapons.FirstOrDefaultAsync(x => x.HeroId == heroId);

            return heroDto;
        }

        public async Task<PlayerDTO> CreateLocalPlayerDTO(Guid playerId)
        {
            var player = await this.GetPlayerById(playerId);
            var cardsInHand = await this.GetCardsInHand(playerId);
            var cardsInDeck = await this.GetCardsInDeck(playerId);
            var heroDTO = await this.CreateHeroDTO(playerId);

            PlayerDTO playerDTO = new PlayerDTO()
            {
                Id = playerId,
                Hero = heroDTO,
                Mana = player.ManaCrystals,
                Minions = player.Minions,
                CardsInDeckCount = cardsInDeck.Count,
                CardsInHand = cardsInHand,
           
            };

            return playerDTO;
        }
        public async Task<PlayerDTO> CreateOpponentPlayerDTO(Guid localPlayerId)
        {
            Player opponent = await this.GetOppositePlayer(localPlayerId);
            Guid opponentId = opponent.Id;
            var cardsInHand = await this.GetCardsInHand(opponentId);

            // cest le opopoenent faque le local doit pas savoir cest quoi les cartes de lopponent
            var cardsInHandAsBlanks = await this.GetBlankCards(cardsInHand);

            var cardsInDeck = await this.GetCardsInDeck(opponentId);
            var heroDTO = await this.CreateHeroDTO(opponentId);

            PlayerDTO playerDTO = new PlayerDTO()
            {
                Id = opponentId,
                Hero = heroDTO,
                Mana = opponent.ManaCrystals,
                Minions = opponent.Minions,
                CardsInDeckCount = cardsInDeck.Count,
                CardsInHand = cardsInHandAsBlanks,
            };

            return playerDTO;
        }

        // va pas marcher pcq ca get un nouveau id si je veux draw \ play la bonne card de la main
        public async Task<List<Card>> GetBlankCards(List<Card> cards)
        {
            var cardsAsBlank = cards
                .Select(c => SampleModels.CreateBlankCard())
                .ToList();
            return cardsAsBlank;
        }
    }
}