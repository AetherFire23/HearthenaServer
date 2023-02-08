using HearthenaServer.CardServices;
using HearthenaServer.Entities;
using Newtonsoft.Json;
namespace WebAPI.GameTasks
{
    public abstract class SpellBase : CardBase
    {
        public SpellBase()
        {

        }

        public abstract bool IsPlayable();
        public abstract bool IsValidTarget();

        public abstract Task ApplySpellEffect(Card card, Dictionary<string, string> targetParameters);

        protected void SomeSharedLogic()
        {

        }
    }
}