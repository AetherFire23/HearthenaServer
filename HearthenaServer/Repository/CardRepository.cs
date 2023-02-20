using AutoMapper;
using HearthenaServer.Constants;
using HearthenaServer.DTO;
using HearthenaServer.Entities;
using HearthenaServer.Extensions;
using HearthenaServer.Interfaces;
using Newtonsoft.Json;
using WebAPI.GameTasks;

namespace HearthenaServer.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly HearthenaContext _context;
        private readonly IMapper _mapper;
        public CardRepository(HearthenaContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Card> GetCardById(Guid cardId)
        {
            var card = _context.Cards.FirstOrDefault(x => x.Id == cardId);
            return card;
        }

        // Are Dependecies resolved upon SavesCHanges() ? No,
        public async Task SetupDummyPlayerAndCards()
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
                PlayerId = ConstDef.Player2Guid,
                // manque weaponId
            };



            Weapon weapon = new Weapon()
            {
                Id = Guid.NewGuid(),
                Attack = 10,
                Charges = 10,
                Type = Enums.WeaponType.FieryWarAxe,
                HeroId = hero1.Id
            };

            // options 

            _context.Heroes.Add(hero1);
            _context.Heroes.Add(hero2);
            _context.Weapons.Add(weapon);

            int z = 0;

            await _context.SaveChangesAsync(); // bug ici ! une reference nest pas correct
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
            await _context.SaveChangesAsync();
        }
    }
}
