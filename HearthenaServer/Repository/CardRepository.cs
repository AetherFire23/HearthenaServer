using HearthenaServer.Constants;
using HearthenaServer.Entities;
using HearthenaServer.Interfaces;
using Newtonsoft.Json;
using WebAPI.GameTasks;

namespace HearthenaServer.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly HearthenaContext _context;
        public CardRepository(HearthenaContext context)
        {
            _context = context;
        }

        public async Task<Card> GetCardById(Guid cardId)
        {
            var card = _context.Cards.FirstOrDefault(x => x.Id == cardId);
            return card;
        }




        public void CreateDummyGame()
        {
            Dictionary<string, string> props = new Dictionary<string, string>()
            {
                {"hp", "2" },
                {"atk", "3" }
            };
            var gameId = Guid.NewGuid();

            var game = new Game()
            {
                Id = gameId,
                Player1Id = ConstDef.Player1Guid,
                Player2Id = ConstDef.Player2Guid,
            };


            var player = new Player()
            {
                Id = ConstDef.Player1Guid,
                ManaCrystals = 0,
                GameId = gameId,
            };

            var player2 = new Player()
            {
                Id = ConstDef.Player2Guid,
                ManaCrystals = 0,
                GameId = gameId,


            };

            var trollCard = new Card()
            {
                Id = ConstDef.Card1Guid,
                Type = GameTaskCode.Troll,
                Properties = props,
                BaseCost = 2,
                CurrentCost = 2,
                IsMinion = true,
                OwnerId = ConstDef.Player1Guid,
                
            };

            var minion = new Minion()
            {
                Health = 0,
                Attack = 0,
                GameTaskCode = GameTaskCode.Troll,
                Id = ConstDef.TrollGuid,
                PlayerId = ConstDef.Player1Guid,
                BoardIndex= 77,
            };

            _context.Minions.Add(minion);
            _context.Games.Add(game);
            _context.Players.Add(player);
            _context.Players.Add(player2);
            _context.Cards.Add(trollCard);
            _context.SaveChanges();
        }
    }
}
