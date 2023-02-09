using HearthenaServer.Interfaces;

namespace HearthenaServer.Entities
{
    public class Hero : IHealth
    {
        public Guid Id { get; set; }
        public int Health { get; set; } = 30;
        public string Name { get; set; } = string.Empty;

        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

    }
}
