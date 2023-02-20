namespace HearthenaServer.Interfaces
{
    public interface ICharacter
    {
        public Guid Id { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }

        public bool IsMinion { get;}
    }
}
