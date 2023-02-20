using HearthenaServer.CardServices;
using HearthenaServer.Entities;
using HearthenaServer.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.GameTasks
{
    public abstract class SpellBase : CardBase
    {
        public SpellBase()
        {

        }

        public abstract bool IsPlayable(GameState gameState);
        public abstract bool IsValidTarget();

        public abstract Task ApplySpellEffect(Card card, Dictionary<string, string> targetParameters);

        protected void SomeSharedLogic()
        {

        }
    }
}