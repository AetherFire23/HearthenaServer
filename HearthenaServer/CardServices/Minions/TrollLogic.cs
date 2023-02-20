using WebAPI.GameTasks;

namespace HearthenaServer.CardServices.Minions
{
    // devrait inherit de genre CardType au lieu de GameTaskType. Les GameTask sont deja 

    [GameTask(GameTaskCode.Troll)]
    public class TrollLogic : MinionBase // MinionBase
    {
        private readonly HearthenaContext _context;
        public TrollLogic(HearthenaContext context) : base()
        {
            _context = context;
        }

        public override bool IsValidAttackTarget(GameTaskContext context)
        {
            return true;
        }

        public override bool IsValidBattleCryTarget(GameTaskContext context)
        {
            return true;
        }

        public override bool IsPlayable()
        {
            return true;

        }

        public override async Task ApplyOnBoardEffect()
        {

        }

        public override async Task ApplyBattleCry(Dictionary<string,string> parameters)
        {

        }

        public override void ApplyDeathRattle()
        {

        }
    }
}