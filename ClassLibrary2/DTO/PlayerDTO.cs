using HearthenaServer.DTO;
using HearthenaServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Models.DTO
{
    public class PlayerDTO
    {
        public Guid Id { get; set; }

        public HeroDTO Hero { get; set; }

        public List<Minion> Minions { get; set; }

        public int Mana { get; set; }

    }
}
