using HearthenaServer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using HearthenaServer.Enums;

namespace HearthenaServer.DTO
{
    // 
    public class HeroDTO // trimmed 
    {
        public Guid Id { get; set; }
        public int Health { get; set; } = 30;
        public string Name { get; set; } = string.Empty;
        public int Attack { get; set; } = 0;



        public Weapon Weapon { get; set; }




        [NotMapped]
        public bool IsMinion => this.GetType() == typeof(Minion);

        [NotMapped]
        public bool HasWeapon => this.Weapon is not null;
    }
}
