using HearthenaServer.Entities;
using HearthenaServer.Interfaces;

namespace HearthenaServer.Services
{
    public class StateRefreshService : IStateRefreshService
    {
        public StateRefreshService()
        {

        }
        public async Task<bool> IsGameEnded(Game game)
        {
            return false;
        }
    }
}
