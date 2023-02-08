﻿namespace HearthenaServer.Entities
{
    public class Game
    {
        public Guid Id { get; set; }

        public Guid Player1Id { get; set; }
        public virtual Player Player1 { get; set; }

        public Guid Player2Id { get; set; }
        public virtual Player Player2 { get; set; }
    }
}
