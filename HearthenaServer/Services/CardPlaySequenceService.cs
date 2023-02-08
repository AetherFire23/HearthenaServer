using HearthenaServer.Constants;
using HearthenaServer.Entities;
using HearthenaServer.Enums;
using HearthenaServer.Extensions;
using HearthenaServer.Interfaces;
using HearthenaServer.Models;
using WebAPI.GameTasks;

namespace HearthenaServer.Services
{
    public class CardPlaySequenceService : ICardPlaySequenceService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPlayerRepository _playerRepository;
        private readonly IBoardRepository _boardRepository;
        private readonly HearthenaContext _context;

        public CardPlaySequenceService(ICardRepository cardRepository, IServiceProvider serviceProvider, IPlayerRepository playerRepository, HearthenaContext context,
            IBoardRepository boardRepository)
        {
            _cardRepository = cardRepository;
            _serviceProvider = serviceProvider;
            _playerRepository = playerRepository;
            _context = context;
            _boardRepository = boardRepository;
        }

        public async Task PlayCard(Guid cardId, Dictionary<string, string> targetParameters)
        {
            Card card = await _cardRepository.GetCardById(cardId);

            bool isMinion = card.IsMinion;
            if (isMinion)
            {
                await PlayMinionSequence(card, targetParameters);
            }

            else
            {
                await PlaySpellSequence(card, targetParameters);
            }
        }

        public async Task PlayMinionSequence(Card card, Dictionary<string, string> targetParameters)
        {
            var serviceType = GameTaskTypeSelector.GetGameTaskType(card.Type);
            var gameTask = _serviceProvider.GetService(serviceType) as MinionBase;

            // Reduce player mana crystals
            var player = await _playerRepository.GetPlayerById(card.OwnerId);
            player.ManaCrystals -= card.CurrentCost;

            // Place on board space
            // Then get a board helper to update the BoardIndexes
            // Cannot have the new minion into the database or else the boardHelper will think it is already on the board.
            var newMinion = card.ToMinion();

            int insertIndex = targetParameters.GetMinionInsertIndex();
            BoardHelper board = player.GetBoard();
            board.InsertMinionInBoardSpace(newMinion, insertIndex);

            // add minion to database
            _context.Add(newMinion);
            await _context.SaveChangesAsync();

            // 
            await _boardRepository.UpdateMinionBoardIndexes(board.Minions);

            // Apply BoardEffects
            await gameTask.ApplyBattleCry(targetParameters);
            await gameTask.ApplyOnBoardEffect();
        }

        public async Task PlaySpellSequence(Card card, Dictionary<string, string> targetParameters)
        {
            var serviceType = GameTaskTypeSelector.GetGameTaskType(card.Type);
            var gameTask = _serviceProvider.GetService(serviceType) as SpellBase; // ass SpellBase

            // reduce mana crystals
            var player = await _playerRepository.GetPlayerById(card.OwnerId);
            player.ManaCrystals -= card.CurrentCost;

            // Cast the spell 
            await gameTask.ApplySpellEffect(card, targetParameters);
        }
    }
}
