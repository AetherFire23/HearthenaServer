﻿// <auto-generated />
using System;
using HearthenaServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HearthenaServer.Migrations
{
    [DbContext(typeof(HearthenaContext))]
    partial class HearthenaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HearthenaServer.Entities.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BaseCost")
                        .HasColumnType("int");

                    b.Property<int>("CurrentCost")
                        .HasColumnType("int");

                    b.Property<string>("IsInHand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsMinion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Properties")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("HearthenaServer.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Player1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Player2Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Player1Id")
                        .IsUnique();

                    b.HasIndex("Player2Id")
                        .IsUnique();

                    b.ToTable("Games");
                });

            modelBuilder.Entity("HearthenaServer.Entities.Hero", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("HearthenaServer.Entities.Minion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("BoardIndex")
                        .HasColumnType("int");

                    b.Property<int>("GameTaskCode")
                        .HasColumnType("int");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Minions");
                });

            modelBuilder.Entity("HearthenaServer.Entities.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HeroId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPlaying")
                        .HasColumnType("bit");

                    b.Property<int>("ManaCrystals")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HeroId")
                        .IsUnique();

                    b.ToTable("Players");
                });

            modelBuilder.Entity("HearthenaServer.Entities.Card", b =>
                {
                    b.HasOne("HearthenaServer.Entities.Player", "Owner")
                        .WithMany("Cards")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("HearthenaServer.Entities.Game", b =>
                {
                    b.HasOne("HearthenaServer.Entities.Player", "Player1")
                        .WithOne()
                        .HasForeignKey("HearthenaServer.Entities.Game", "Player1Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HearthenaServer.Entities.Player", "Player2")
                        .WithOne()
                        .HasForeignKey("HearthenaServer.Entities.Game", "Player2Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Player1");

                    b.Navigation("Player2");
                });

            modelBuilder.Entity("HearthenaServer.Entities.Minion", b =>
                {
                    b.HasOne("HearthenaServer.Entities.Player", "Player")
                        .WithMany("Minions")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("HearthenaServer.Entities.Player", b =>
                {
                    b.HasOne("HearthenaServer.Entities.Hero", "Hero")
                        .WithOne("Player")
                        .HasForeignKey("HearthenaServer.Entities.Player", "HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hero");
                });

            modelBuilder.Entity("HearthenaServer.Entities.Hero", b =>
                {
                    b.Navigation("Player")
                        .IsRequired();
                });

            modelBuilder.Entity("HearthenaServer.Entities.Player", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("Minions");
                });
#pragma warning restore 612, 618
        }
    }
}
