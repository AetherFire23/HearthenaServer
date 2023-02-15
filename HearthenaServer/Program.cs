using HearthenaServer;
using HearthenaServer.Constants;
using HearthenaServer.Entities;
using HearthenaServer.Enums;
using HearthenaServer.Interfaces;
using HearthenaServer.Repository;
using HearthenaServer.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;
using WebAPI.GameTasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardPlaySequenceService, CardPlaySequenceService>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<ITurnService, TurnService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ITradeService, TradeService>();


RegisterGameTaskTypes(builder);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add db context here
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<HearthenaContext>(options
=> options.UseSqlServer(connectionString));


var app = builder.Build();

//Migration
using (var scope = app.Services.CreateScope())
{

    // this deletes and recreates the whole databse when it is launched.
    var context = scope.ServiceProvider.GetService<HearthenaContext>();
    context.Database.EnsureDeleted(); // deocher our recommecner
    context.Database.Migrate();


    var cardRepository = scope.ServiceProvider.GetService<ICardRepository>();
    var cardPlayService = scope.ServiceProvider.GetService<ICardPlaySequenceService>();
    ITurnService turnservice = scope.ServiceProvider.GetService<ITurnService>();
    IPlayerRepository playerRepository = scope.ServiceProvider.GetService<IPlayerRepository>();
    ITradeService tradeService = scope.ServiceProvider.GetService<ITradeService>();




    // setyp
    cardRepository.SetupDummyPlayerAndCards();
    var dummyGame = context.Games.FirstOrDefault();
    var playingPlayer = await playerRepository.GetPlayingPlayer(dummyGame);

    // play troll at index 0 (first)
    var trollcard = playingPlayer.Cards.FirstOrDefault(c => c.IsMinion && c.OwnerId == playingPlayer.Id);
    var minionIndex = new Dictionary<string, string>()
    {
        {StringParameters.MinionInsertIndex, "0" }
    };
    await cardPlayService.PlayCard(trollcard.Id, minionIndex);


    // get troll reference and hero reference
    var troll = playingPlayer.Minions.First();
    var p1hero = await tradeService.GetTarget(dummyGame.Player1.Hero.Id);

    var test = Enchant.TryGetEnchant<bool>(EnchantmentType.DivineShield);

    int i = 0;
   // 

    //await tradeService.TradeCharacters(p1hero, troll);



    //await turnservice.BeginGame(dummyGame);

    //Player first = await playerRepository.GetPlayingPlayer(dummyGame);
    //var trollcard = first.Cards.FirstOrDefault(c => c.IsMinion && c.OwnerId == first.Id);
    //var minionIndex = new Dictionary<string, string>()
    //{
    //    {StringParameters.MinionInsertIndex, "0" }
    //};

    //await cardPlayService.PlayCard(trollcard.Id, minionIndex);
    //await turnservice.EndTurn(dummyGame);
    //await turnservice.BeginTurn(dummyGame);

    //// setup second card to be a spell 
    //var oppositeMinon = context.Minions.FirstOrDefault(x => x.PlayerId == first.Id);
    //var fireSpellTargetParameters = Card.CreateTargetParameters(oppositeMinon.Id, typeof(Minion));
    //// problem : chosen randomly ! 
    //var second = await playerRepository.GetPlayingPlayer(dummyGame);
    //var firstFireSpell = second.Cards.FirstOrDefault(c => !c.IsMinion);

    //await cardPlayService.PlayCard(firstFireSpell.Id, fireSpellTargetParameters);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void RegisterGameTaskTypes(WebApplicationBuilder builder)
{
    var types = typeof(IGameTask).Assembly.GetTypes()
        .Where(type => type.IsClass && !type.IsAbstract
            && typeof(IGameTask).IsAssignableFrom(type)
            && CustomAttributeExtensions.GetCustomAttribute<GameTaskAttribute>(type) != null);

    foreach (var type in types)
    {
        builder.Services.AddTransient(type);
    }
}