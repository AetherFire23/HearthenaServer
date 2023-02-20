using HearthenaServer.DTO;
using HearthenaServer.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearthenaServer.Entities
{
    public class Hero : ICharacter
    {
        public Guid Id { get; set; }
        public int Health { get; set; } = 30;
        public string Name { get; set; } = string.Empty;
        public int Attack { get; set; } = 0;
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [NotMapped]
        public bool IsMinion => this.GetType() == typeof(Minion);

       
    }
}
