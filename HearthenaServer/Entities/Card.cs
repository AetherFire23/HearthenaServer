using HearthenaServer.Constants;
using HearthenaServer.Enums;
using Newtonsoft.Json;
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
        public virtual Player Owner { get; set; }


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

        public static Dictionary<string, string> CreateTargetParameters(Guid targetId, Type targetType)
        {
            var fireSpellTargetParameters = new Dictionary<string, string>()
            {
                {StringParameters.TargetId, targetId.ToString() },
                { StringParameters.TargetType, JsonConvert.SerializeObject(targetType) }
            };

            return fireSpellTargetParameters;
        }
    }
}
