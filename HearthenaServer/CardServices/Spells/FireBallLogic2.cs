//using HearthenaServer.Entities;
//using HearthenaServer.Extensions;
//using HearthenaServer.Interfaces;
//using Newtonsoft.Json;
//using Shared_Models.UnityCardsLogic;
//using WebAPI.GameTasks;

//namespace HearthenaServer.CardServices.Spells
//{
//    [GameTask(GameTaskCode.FireBall)]
//    public class FireBallLogic2 : FireSpellBase // abstract pr impleter ds unity 
//    {
//        private readonly IPlayerRepository _playerRepository;
//        private readonly HearthenaContext _context;

//        public FireBallLogic2()
//        {

//        }

//        public FireBallLogic2(HearthenaContext context, IPlayerRepository playerRepository)
//        {
//            _context = context;
//            _playerRepository = playerRepository;
//        }

//        public override bool IsPlayable()
//        {
//            return base.IsPlayable();
//        }

//        public override bool IsValidTarget() // implemented ds le abstract
//        {
//            return base.IsValidTarget();
//        }

//        public override async Task ApplySpellEffect(Card card, Dictionary<string, string> targetParameters)
//        {
//            int damage = card.Properties.GetSpellDamage();
//            ICharacter attackedTarget = await _playerRepository.GetTarget(targetParameters).ConfigureAwait(false);
//            attackedTarget.Health -= damage;
//            await _context.SaveChangesAsync().ConfigureAwait(false);
//        }
//    }
//}
