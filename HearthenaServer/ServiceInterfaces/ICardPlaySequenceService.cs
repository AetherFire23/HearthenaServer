namespace HearthenaServer.Interfaces
{
    public interface ICardPlaySequenceService
    {
        public Task PlayCard(Guid cardId, Dictionary<string, string> targetParameters);

    }
}
