using HearthenaServer.DTO;
using HearthenaServer.Entities;
using HearthenaServer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPI.GameTasks;

namespace HearthenaServer.Services
{
    public class TradeService : ITradeService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPlayerRepository _playerRepository;
        private readonly IBoardRepository _boardRepository;
        private readonly HearthenaContext _context;

        public TradeService(ICardRepository cardRepository, IServiceProvider serviceProvider, IPlayerRepository playerRepository, HearthenaContext context,
            IBoardRepository boardRepository,
            IServiceRepository serviceRepository)
        {
            _cardRepository = cardRepository;
            _serviceProvider = serviceProvider;
            _playerRepository = playerRepository;
            _context = context;
            _boardRepository = boardRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task MinionAttackMinion(ICharacter attacker, ICharacter attacked)
        {
            await this.RemoveHealth(attacker, attacked);
            await this.RemoveHealth(attacked, attacker);

            

            await _context.SaveChangesAsync();
        }

        public async Task MinionAttackFace(ICharacter attacker, ICharacter attacked)
        {
            attacked.Health -= attacker.Attack;
            await _context.SaveChangesAsync();
        }

        public async Task HeroAttackMinion(ICharacter attacker, ICharacter attacked)
        {
            Hero hero = await _context.Heroes.FirstOrDefaultAsync(x => x.Id == attacker.Id);

            HeroDTO heroAsDTO = await _playerRepository.CreateHeroDTO(hero.Id);

            // should make GetTotalDamage()


            int totalDamage = heroAsDTO.HasWeapon
                ? heroAsDTO.Attack + heroAsDTO.Weapon.Attack
                : heroAsDTO.Attack;

            // check for divine Shield ? 
            attacked.Health -= totalDamage;
        }

        public async Task HeroAttackFace(ICharacter attacker, ICharacter attacked)
        {

        }

        public async Task TradeCharacters(ICharacter attacker, ICharacter attacked)
        {
            if (attacker.IsMinion && attacked.IsMinion) await MinionAttackMinion(attacker, attacked);
            else if (attacker.IsMinion && !attacked.IsMinion) await MinionAttackFace(attacker, attacked);
            else if (!attacker.IsMinion && attacked.IsMinion) await HeroAttackMinion(attacker, attacked);
            else if (!attacker.IsMinion && !attacked.IsMinion) await HeroAttackFace(attacker, attacked);
        }

        public async Task RemoveHealth(ICharacter attacker, ICharacter attacked) // sur le model
        {
            // make checks to know whether or not some action must be done before dealing damage.
            // ex. : Poisons, divine shield
            // However this should be dome on gameTaskLogic like .OnDealDamage()

            attacked.Health -= attacked.Attack;
            await _context.SaveChangesAsync();

        }
    }
}