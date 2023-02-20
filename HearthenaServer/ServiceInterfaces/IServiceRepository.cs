using HearthenaServer.Entities;
using WebAPI.GameTasks;

namespace HearthenaServer.Interfaces
{
    public interface IServiceRepository
    {
        public Task<object> GetGameTaskService(Card card);
        public Task<object> GetGameTaskService(GameTaskCode taskCode);
    }
}
