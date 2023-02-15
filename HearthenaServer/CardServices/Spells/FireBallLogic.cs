using HearthenaServer.Entities;
using HearthenaServer.Extensions;
using HearthenaServer.Interfaces;
using Newtonsoft.Json;
using WebAPI.GameTasks;

namespace HearthenaServer.CardServices.Spells
{
    [GameTask(GameTaskCode.FireBall)]
    public class FireBallLogic : SpellBase
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly HearthenaContext _context;
        public FireBallLogic(HearthenaContext context, IPlayerRepository playerRepository)
        {
            _context = context;
            _playerRepository = playerRepository;
        }

        public override bool IsPlayable()
        {
            return false;
        }
        public override bool IsValidTarget()
        {
            return false;
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
