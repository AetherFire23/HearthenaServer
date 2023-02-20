using HearthenaServer.Models;
using Shared_Models.DTO;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace HearthenaServer.Entities
{
    public class Player
    {
        public Guid Id { get; set; }
        public int ManaCrystals { get; set; }
        public bool IsPlaying {get; set;}

        // Navigation
        public virtual List<Minion> Minions { get; set; }
        public Guid GameId { get; set; } // currently not used
        public List<Card> Cards { get; set; }

        public Guid HeroId { get; set; }
        public virtual Hero Hero { get; set; }
    }



}
