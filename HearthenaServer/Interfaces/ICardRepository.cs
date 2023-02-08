using HearthenaServer.Entities;

namespace HearthenaServer.Interfaces
{
    public interface ICardRepository
    {
        public void CreateDummyGame();
        public Task<Card> GetCardById(Guid cardId);

    }
}
