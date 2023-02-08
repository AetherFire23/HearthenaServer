using HearthenaServer.Interfaces;
using WebAPI.GameTasks;

namespace HearthenaServer.Entities
{
    public class Minion : IHealth
    {
        public Guid Id { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public GameTaskCode GameTaskCode { get; set; }
        public int BoardIndex { get; set; }

        // navigation
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
