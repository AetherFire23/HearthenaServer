using HearthenaServer.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using WebAPI.GameTasks;

namespace HearthenaServer
{
    public class HearthenaContext : DbContext
    {

        // Must have constructor to work
        public HearthenaContext(DbContextOptions<HearthenaContext> options) : base(options)
        {

        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Minion> Minions { get; set; }
        public DbSet<Hero> Heroes{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                 .Property(p => p.Properties)
                 .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v));

            this.InitializePlayerEntityBuilder(modelBuilder);

            // Declares Game has two players
            // Players dont have their game because it causes infinite regression for some reason when
            // not using a virtual List<Player>().
            modelBuilder.Entity<Game>()
                .HasOne(p => p.Player1)
                .WithOne()
                .HasForeignKey<Game>(p => p.Player1Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Game>()
                .HasOne(p => p.Player2)
                .WithOne()
                .HasForeignKey<Game>(p => p.Player2Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Card>()
                .Property(p => p.Type)
                .HasConversion(
                v => v.ToString(),
                v => (GameTaskCode)Enum.Parse(typeof(GameTaskCode), v)
                );

            modelBuilder.Entity<Card>()
                .Property(p => p.IsInHand)
                .HasConversion(
                v => v.ToString(),
                v => Convert.ToBoolean(v)
                );

                        modelBuilder.Entity<Card>()
                .Property(p => p.IsMinion)
                .HasConversion(
                v => v.ToString(),
                v => Convert.ToBoolean(v)
                );
        }

        private void InitializePlayerEntityBuilder(ModelBuilder modelBuilder)
        {
            // Declares player has card
            var playerEntity = modelBuilder.Entity<Player>();

            playerEntity
                .HasMany(p => p.Cards)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            // Declares aplyer has minions
            playerEntity
                .HasMany(p => p.Minions)
                .WithOne(p => p.Player)
                .HasForeignKey(k => k.PlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            // setting which generic type to the HasForeignKey<> method defines which entity must be set first.
            playerEntity
                .HasOne(p => p.Hero)
                .WithOne(p => p.Player)
                .HasForeignKey<Player>(e => e.HeroId);
        }
    }
}
