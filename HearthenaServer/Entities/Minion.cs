using HearthenaServer.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.GameTasks;

namespace HearthenaServer.Entities
{
    public class Minion : ICharacter
    {
        public Guid Id { get; set; }
        // public int BaseAttack {get; set;}
        public int Attack { get; set; }
        public int Health { get; set; }
        public GameTaskCode GameTaskCode { get; set; }
        public int BoardIndex { get; set; }

        public Dictionary<string, string> Enchantments = new();

        // navigation
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }


        [NotMapped]
        public bool IsMinion => this.GetType() == typeof(Minion);
    }
}
