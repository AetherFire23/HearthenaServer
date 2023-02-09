using HearthenaServer.Entities;

namespace HearthenaServer.Interfaces
{
    public interface ITurnService
    {
        public Task BeginGame(Game game);
        public  Task ChooseFirstPlayer(Game game);

        public  Task HandOutFirstCards(Game game);
        public Task BeginTurn(Game game);

        public Task EndTurn(Game game);
        public  Task HandleTurnChecks(Game game);
        public  Task SwapPlayingCharacters(Game game);

    }
}
