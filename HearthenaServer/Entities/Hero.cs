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

        // nav
        public Guid WeaponId { get; set; }
        public virtual Weapon Weapon { get; set; }
        public Guid PlayerId { get; set; }

        public virtual Player Player { get; set; }

        [NotMapped]
        public bool IsMinion => this.GetType() == typeof(Minion);

        [NotMapped]
        public bool HasWeapon => this.Weapon is not null;
    }
}
