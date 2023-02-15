using HearthenaServer.Enums;

namespace HearthenaServer.Entities
{
    public class Weapon
    {
        public Guid Id { get; set; }
        public WeaponType Type { get; set; }
        public int Attack { get; set; }
        public int Charges { get; set; }
        // nav
        public Guid HeroId{ get; set; }
        public Hero Hero { get; set; }
    }
}
