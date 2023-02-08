using HearthenaServer;
using HearthenaServer.Constants;
using HearthenaServer.Interfaces;
using HearthenaServer.Repository;
using HearthenaServer.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPI.GameTasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardPlaySequenceService, CardPlaySequenceService>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IBoardRepository, BoardRepository>();

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
    var playerContextService = scope.ServiceProvider.GetService<HearthenaContext>();
    playerContextService.Database.EnsureDeleted(); // deocher our recommecner
    playerContextService.Database.Migrate();


    var gameMakerService = scope.ServiceProvider.GetService<ICardRepository>();
    var cardService = scope.ServiceProvider.GetService<ICardPlaySequenceService>();



    var target = new Dictionary<string, string>()
    {
        {StringParameters.MinionInsertIndex, "4" }
    };



    gameMakerService.CreateDummyGame();


    // Determine who got the first turn
    // Draw first cards (draw more for nd player)

    // First player plays a minion
    
    // End turn checks
    // Begin turn actions

    // 2nd player plays a spell on heros face

    // end turn (check for game end)



   // await cardService.PlayCard(ConstDef.Card1Guid, target);

    // GameMakerService

    // 






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