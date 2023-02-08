using WebAPI.GameTasks;

namespace HearthenaServer.CardServices
{
    public abstract class CardBase : IGameTask
    {
        // cardBase should inherit injected services
        protected bool HasSufficientMana(Dictionary<string, string> props)
        {
            return true;
        }
    }
}
