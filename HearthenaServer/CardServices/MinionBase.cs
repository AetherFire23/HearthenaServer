using HearthenaServer.CardServices;
using HearthenaServer.Interfaces;
using Newtonsoft.Json;
namespace WebAPI.GameTasks
{
    public abstract class MinionBase : CardBase
    {
        public MinionBase()
        {

        }

        public abstract bool IsValidAttackTarget(GameTaskContext context);
        public abstract bool IsValidBattleCryTarget(GameTaskContext context);
        public abstract bool IsPlayable();
        public abstract Task ApplyOnBoardEffect();
        public abstract Task ApplyBattleCry(Dictionary<string, string> parameters);
        public abstract void ApplyDeathRattle();

        // ApplyDivineSHield
        protected void ApplyOnDamageEffect(ICharacter other, ICharacter self) // IDamageSource ? 
        {
            // if Properties.HasDivineSHield
            // if (Properties.Has


            
        }

        protected void OnDealDamage(ICharacter attacker)
        {

        }

        protected void SomeSharedLogic()
        {

        }

        // should be other repo for now
        protected bool HasTaunts()
        {
            return true;
        }

        protected bool HasDivineShield()
        {
            return true;
        }
    }
}