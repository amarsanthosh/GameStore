// using GameStore.API.Dtos;
using GameStore.API.Data;
using GameStore.API.Endpoints;
var builder = WebApplication.CreateBuilder(args);
var ConnString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(ConnString);
var app = builder.Build();

app.MapGamesEndpoints();
app.MigrateDb();
app.MapGenreEndpoints();
app.Run();
