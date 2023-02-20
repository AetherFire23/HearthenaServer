using HearthenaServer.Entities;
using System.Collections.Generic;
using WebAPI.GameTasks;

namespace HearthenaServer.CardServices
{
    public abstract class CardBase : IGameTask
    {
        // cardBase should inherit injected services
        protected bool HasSufficientMana(Player p, Card c)
        {
            return p.ManaCrystals >= c.CurrentCost;
        }
    }
}
