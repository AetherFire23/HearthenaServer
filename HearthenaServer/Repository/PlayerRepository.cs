using HearthenaServer.Entities;
using HearthenaServer.Interfaces;
using System.Runtime.Intrinsics.X86;

namespace HearthenaServer.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly HearthenaContext _context;
        public PlayerRepository(HearthenaContext context)
        {
            _context = context;
        }
        public async Task<Player> GetPlayerById(Guid id)
        {
            var player = _context.Players.FirstOrDefault(p => p.Id == id);
            return player;
        }

        public async Task<Game> GetGameById(Guid gameId)
        {
            return _context.Games.FirstOrDefault(x => x.Id == gameId);
        }

        public async Task<Player> GetPlayingPlayer(Game game)
        {
            return game.Player1.IsPlaying
            ? game.Player1
            : game.Player2;
        }

        public async Task<Player> GetNonPlayingPlayer(Game game)
        {
            return !game.Player1.IsPlaying
            ? game.Player1
            : game.Player2;
        }

        public async Task<List<Card>> GetCardsInHand(Player player)
        {
            var cardsInHand = _context.Cards.Where(c => c.IsInHand && c.OwnerId == player.Id).ToList();
            return cardsInHand;
        }
        public async Task<List<Card>> GetCardsInDeck(Player player)
        {
            var cardsInDeck = _context.Cards.Where(c => c.IsInHand == false && c.OwnerId == player.Id).ToList();
            return cardsInDeck;
        }
    }
}