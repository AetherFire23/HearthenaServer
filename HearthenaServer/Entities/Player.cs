using HearthenaServer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace HearthenaServer.Entities
{
    public class Player
    {
        public Guid Id { get; set; }
        public int ManaCrystals { get; set; }
        public List<Card> AllCards { get; set; }
        public bool IsPlaying {get; set;}
        // Navigation
        public virtual List<Minion> Minions { get; set; }

        public Guid GameId { get; set; }



        public BoardHelper GetBoard()
        {
            return new BoardHelper(this.Minions);
        }
    }
}
