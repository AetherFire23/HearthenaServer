using HearthenaServer.CardServices;
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

        protected void SomeSharedLogic()
        {

        }

        // should be other repo for now
        protected bool HasTaunts()
        {
            return true;
        }
    }
}