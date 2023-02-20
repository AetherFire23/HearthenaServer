using HearthenaServer.Entities;

namespace HearthenaServer.Interfaces
{
    public interface ICardRepository
    {
        public Task<Card> GetCardById(Guid cardId);
        public Task SetupDummyPlayerAndCards();
    }
}
