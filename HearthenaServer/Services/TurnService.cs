using HearthenaServer.Entities;
using HearthenaServer.Interfaces;
using HearthenaServer.Utils;

namespace HearthenaServer.Services
{
    public class TurnService : ITurnService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly HearthenaContext _context;
        private readonly IPlayerRepository _playerRepository;

        public TurnService(IServiceProvider serviceProvider,
                            HearthenaContext context,
                            IPlayerRepository playerRepository)
        {
            _serviceProvider = serviceProvider;
            _context = context;
            _playerRepository = playerRepository;
        }

        public async Task BeginGame(Game game)
        {
            await ChooseFirstPlayer(game);
            await HandOutCards(game);
            await BeginTurn(game);
        }

        public async Task ChooseFirstPlayer(Game game)
        {
            Player startingPlayer = Rand.r.Next(0, 2) == 1
                ? game.Player1
                : game.Player2;

            startingPlayer.IsPlaying = true;
        }

        public async Task HandOutCards(Game game)
        {

        }

        public async Task BeginTurn(Game game)
        {
            var playing = _playerRepository.GetPlayingPlayer(game);

            // program cards

        }

        public async Task EndTurn(Game game)
        {
            await SwapPlayingCharacters(game);
            await HandleTurnChecks(game);
        }

        public async Task HandleTurnChecks(Game game)
        {

        }

        public async Task SwapPlayingCharacters(Game game)
        {
            var playingPlayer = await _playerRepository.GetPlayingPlayer(game);
            var nonPlayingPlayer = await _playerRepository.GetNonPlayingPlayer(game);

            playingPlayer.IsPlaying = false;
            nonPlayingPlayer.IsPlaying = true;
            await _context.SaveChangesAsync();
        }
    }
}
