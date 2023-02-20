namespace HearthenaServer.Interfaces
{
    public interface ITradeService
    {
        public Task TradeCharacters(ICharacter attacker, ICharacter attacked);
    }
}
