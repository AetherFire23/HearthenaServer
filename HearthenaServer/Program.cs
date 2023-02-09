using HearthenaServer;
using HearthenaServer.Constants;
using HearthenaServer.Entities;
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



    //var target = new Dictionary<string, string>()
    //{
    //    {StringParameters.MinionInsertIndex, "4" }
    //};

    cardRepository.SetupDummyPlayerAndCards();
    var dummyGame = context.Games.FirstOrDefault();
    await turnservice.BeginGame(dummyGame);

    Player first = await playerRepository.GetPlayingPlayer(dummyGame);
    var firstplayerCard = first.Cards.FirstOrDefault(c => c.IsMinion);
    var targetParameters = new Dictionary<string, string>()
    {
        {StringParameters.MinionInsertIndex, "0" }
    };

    await cardPlayService.PlayCard(firstplayerCard.Id, targetParameters);
    await turnservice.EndTurn(dummyGame);
    await turnservice.BeginTurn(dummyGame);

    // setup second card to be a spell 
    var oppositeMinon = context.Minions.FirstOrDefault(x => x.PlayerId == first.Id);
    var fireSpellTargetParameters = new Dictionary<string, string>()
    {
        { StringParameters.TargetId, oppositeMinon.Id.ToString()},
        { StringParameters.TargetType, JsonConvert.SerializeObject(typeof(Minion))}

    };
    // problem : chosen randomly ! 
    var second = await playerRepository.GetPlayingPlayer(dummyGame);
    var firstFireSpell = second.Cards.FirstOrDefault(c => !c.IsMinion);

    await cardPlayService.PlayCard(firstFireSpell.Id, fireSpellTargetParameters);



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