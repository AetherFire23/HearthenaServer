using HearthenaServer.Enums;
using WebAPI.GameTasks;

namespace HearthenaServer.Entities
{
    public class Card
    {
        public Guid Id { get; set; }
        public GameTaskCode Type { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new();

        /// <summary>
        /// Default = true
        /// </summary>
        public bool IsInHand { get; set; } = false;

        public bool IsMinion { get; set; }

        public int BaseCost { get; set; }

        public int CurrentCost { get; set; }

        public Guid OwnerId { get; set; }
        public Player Owner { get; set; }

        //public bool IsMinion()
        //{
        //    bool isMinion = this.Properties.GetValueOrDefault("isMinion") == "True";
        //    return isMinion;
        //}

        // public bool IsMinion => this.Properties.GetValueOrDefault("isMinion") == "True";

        public Minion ToMinion()
        {
            int attackValue = Convert.ToInt32(Properties.GetValueOrDefault("atk"));
            int hp = Convert.ToInt32(Properties.GetValueOrDefault("hp"));
            var newMinion = new Minion()
            {
                Id = Guid.NewGuid(),
                PlayerId = OwnerId,
                GameTaskCode = Type,
                Attack = attackValue,
                Health = hp,
                BoardIndex = -1
            };
            return newMinion;
        }
    }
}
