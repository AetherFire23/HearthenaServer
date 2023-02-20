using HearthenaServer.Entities;
using HearthenaServer.Extensions;
using HearthenaServer.Interfaces;
using HearthenaServer.Models;
using Newtonsoft.Json;
using Shared_Models.UnityCardsLogic;
using WebAPI.GameTasks;

namespace HearthenaServer.CardServices.Spells
{
    [GameTask(GameTaskCode.FireBall)]
    public class FireBallLogic : FireSpellBase // abstract pr impleter ds unity 
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly HearthenaContext _context;

        public FireBallLogic()
        {

        }

        public FireBallLogic(HearthenaContext context, IPlayerRepository playerRepository)
        {
            _context = context;
            _playerRepository = playerRepository;
        }

        public override bool IsPlayable(GameState gameState)
        {
            return base.IsPlayable(gameState);
        }

        public override bool IsValidTarget() // implemented ds le abstract
        {
            return base.IsValidTarget();
        }

        public override async Task ApplySpellEffect(Card card, Dictionary<string, string> targetParameters)
        {
            int damage = card.Properties.GetSpellDamage();
            ICharacter attackedTarget = await _playerRepository.GetTarget(targetParameters).ConfigureAwait(false);
            attackedTarget.Health -= damage;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
