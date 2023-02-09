using HearthenaServer.Constants;
using HearthenaServer.Entities;
using HearthenaServer.Extensions;
using HearthenaServer.Interfaces;
using HearthenaServer.Utils;
using System.Collections;

namespace HearthenaServer.Services
{
    // all card draws are random for now because there is not Deck ordering.
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
            await HandOutFirstCards(game);
            await BeginTurn(game);
        }

        public async Task ChooseFirstPlayer(Game game)
        {
            Player startingPlayer = Rand.r.Next(0, 2) == 1
                ? game.Player1
                : game.Player2;

            startingPlayer.IsPlaying = true;
        }

        public async Task HandOutFirstCards(Game game)
        {
            // Get the players Deck are shuffle it
            Player firstPlayer = await _playerRepository.GetPlayingPlayer(game);
            Player secondPlayer = await _playerRepository.GetNonPlayingPlayer(game);

            await this.ShuffleAndDrawCards(firstPlayer, ConstDef.FirstPlayerStartingHandCount);
            await this.ShuffleAndDrawCards(secondPlayer, 2);
        }

        public async Task BeginTurn(Game game)
        {
            var playing = await _playerRepository.GetPlayingPlayer(game);
            await DrawCard(playing);
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

        private async Task ShuffleAndDrawCards(Player player, int cardAmount)
        {
            List<Card> cardsInDeck = await _playerRepository.GetCardsInDeck(player);
            List<Card> shuffled = cardsInDeck.Shuffle();

            for (int i = 0; i < cardAmount; i++)
            {
                var card = shuffled.ElementAt(i);
                card.IsInHand = false;
            }
            await _context.SaveChangesAsync();
        }

        private async Task DrawCard(Player player)
        {
            await ShuffleAndDrawCards(player, 1);
        }
    }
}
