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
        private readonly HearthenaContext _context;
        public FireBallLogic(HearthenaContext context)
        {
            _context = context;
        }

        public override bool IsPlayable()
        {
            return false;
        }
        public override bool IsValidTarget()
        {
            return false;
        }


        // targetType - API
        // targetId - Unity 

        // damage - API (card)
        public override async Task ApplySpellEffect(Card card, Dictionary<string, string> targetParameters)
        {
            Guid targetId = targetParameters.GetTargetId();
            int damage = card.Properties.GetSpellDamage();

            Type targetType = targetParameters.GetTargetType();


            // may not be tracked because of casting
            IHealth attackedTarget = targetType == typeof(Minion)
                ? _context.Minions.FirstOrDefault(x => x.Id == targetId) as IHealth
                : _context.Heroes.FirstOrDefault(x => x.Id == targetId) as IHealth;

            attackedTarget.Health -= damage;
            await _context.SaveChangesAsync();
        }
    }
}
