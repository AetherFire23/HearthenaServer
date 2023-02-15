using HearthenaServer.Entities;
using HearthenaServer.Interfaces;
using WebAPI.GameTasks;

namespace HearthenaServer.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public ServiceRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<object> GetGameTaskService(Card card)
        {
            var gameTask = this.GetGameTaskService(card.Type);
            return gameTask;
        }

        public async Task<object> GetGameTaskService(GameTaskCode taskCode)
        {
            var serviceType = GameTaskTypeSelector.GetGameTaskType(taskCode);
            var gameTask = _serviceProvider.GetService(serviceType);
            return gameTask;
        }
    }
}
