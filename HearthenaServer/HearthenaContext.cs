using HearthenaServer.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

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
            //modelBuilder.Entity<Card>()
            //     .Property(p => p.Properties)
            //     .HasConversion(
            //    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v),
            //    v => JsonConvert.SerializeObject(v));


            modelBuilder.Entity<Card>()
                 .Property(p => p.Properties)
                 .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v));

            modelBuilder.Entity<Player>()
                .HasMany(p => p.AllCards)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Minions)
                .WithOne(p => p.Player)
                .HasForeignKey(k => k.PlayerId)
                .OnDelete(DeleteBehavior.NoAction);

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
        }
    }
}
