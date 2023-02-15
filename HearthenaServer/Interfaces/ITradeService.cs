namespace HearthenaServer.Interfaces
{
    public interface ITradeService
    {
        public Task<ICharacter> GetTarget(Guid targetId);
        public Task TradeCharacters(ICharacter attacker, ICharacter attacked);
    }
}
