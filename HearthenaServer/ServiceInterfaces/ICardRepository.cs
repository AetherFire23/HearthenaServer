using HearthenaServer.Entities;

namespace HearthenaServer.Interfaces
{
    public interface ICardRepository
    {
        public void SetupDummyPlayerAndCards();
        public Task<Card> GetCardById(Guid cardId);

    }
}
