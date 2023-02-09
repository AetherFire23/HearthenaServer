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

        // Are Dependecies resolved upon SavesCHanges() ? No,
        public async void SetupDummyPlayerAndCards()
        {
            var game = new Game()
            {
                Id = ConstDef.GameId,
                Player1Id = ConstDef.Player1Guid,
                Player2Id = ConstDef.Player2Guid,
                
            };
            var player = new Player()
            {
                Id = ConstDef.Player1Guid,
                GameId = ConstDef.GameId,
                ManaCrystals = 0,
                IsPlaying = false,
                HeroId = ConstDef.Hero1Guid
            };

            var player2 = new Player()
            {
                Id = ConstDef.Player2Guid,
                GameId = ConstDef.GameId,
                ManaCrystals = 0,
                IsPlaying = false,
                HeroId = ConstDef.Hero2Guid

            };

            var hero1 = new Hero()
            {
                Id = ConstDef.Hero1Guid,
                Health = 30,
                Name = "Yeah",
                PlayerId = ConstDef.Player1Guid
            };

            var hero2 = new Hero()
            {
                Id = ConstDef.Hero2Guid,
                Health = 30,
                Name = "Yea2h",
                PlayerId = ConstDef.Player2Guid
            };

            _context.Heroes.Add(hero1);
            _context.Heroes.Add(hero2);

            int z = 0;

            await _context.SaveChangesAsync();
            _context.Players.Add(player);
            _context.Players.Add(player2);

            await _context.SaveChangesAsync();







            _context.Games.Add(game);
            await _context.SaveChangesAsync();


            for (int i = 0; i < 15; i++)
            {
                var card = new Card()
                {
                    Id = Guid.NewGuid(),
                    BaseCost = 0,
                    CurrentCost= 0,
                    IsInHand = false,
                    IsMinion = true,
                    OwnerId = ConstDef.Player1Guid,
                    Properties = CardProperties.TrollProperties,
                    Type = GameTaskCode.Troll,
                };
                _context.Cards.Add(card);
            }

            for (int i = 0; i < 15; i++)
            {
                var card = new Card()
                {
                    Id = Guid.NewGuid(),
                    BaseCost = 0,
                    CurrentCost = 0,
                    IsInHand = false,
                    IsMinion = true,
                    OwnerId = ConstDef.Player2Guid,
                    Properties = CardProperties.TrollProperties,
                    Type = GameTaskCode.Troll,
                };
                _context.Cards.Add(card);

            }

            for (int i = 0; i < 15; i++)
            {
                var fireSPell = new Card()
                {
                    Id = Guid.NewGuid(),
                    BaseCost = 0,
                    CurrentCost = 0,
                    IsMinion = false,
                    OwnerId = ConstDef.Player1Guid,
                    Properties = CardProperties.FireBallProperties,
                    Type = GameTaskCode.FireBall,
                    IsInHand= false,
                };

                _context.Cards.Add(fireSPell);
            }
            for (int i = 0; i < 15; i++)
            {
                var fireSPell = new Card()
                {
                    Id = Guid.NewGuid(),
                    BaseCost = 0,
                    CurrentCost = 0,
                    IsMinion = false,
                    OwnerId = ConstDef.Player2Guid,
                    Properties = CardProperties.FireBallProperties,
                    Type = GameTaskCode.FireBall,
                    IsInHand= false,
                    
                };

                _context.Cards.Add(fireSPell);
            }
            _context.SaveChanges();
        }
    }
}
