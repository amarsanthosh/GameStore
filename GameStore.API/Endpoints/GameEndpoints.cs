using GameStore.API.Data;
using GameStore.API.Dtos;
using GameStore.API.Entities;
using GameStore.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints;

public static class GameEndpoints
{
    
private static readonly List<GameDto> games = [
    new(
        1,
        "Street Fighting",
        "combat",
        19.19M,
        new DateOnly(1992,7,15)
    ), 
    new(
        2 ,
        "fifa",
        "sports",
        69.99M,
        new DateOnly(2022,9,27)
    )
];


    const string GetGameEndPointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("games")
                        .WithParameterValidation();
        // GET /games
        group.MapGet("/", async (GameStoreContext dbContext) =>await dbContext.Games
                                                        .Include(game => game.Genre)
                                                        .Select(game => game.ToDto())
                                                        .AsNoTracking()
                                                        .ToListAsync());

        // GET /games/1
        group.MapGet("/{id}", async (int id , GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndPointName);

        //POST /games
        group.MapPost("/", async(CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();
            game.Genre = dbContext.Genres.Find(newGame.GenreId);    

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game.ToGameDetailsDto());
        });
        //PUT /games/1
        group.MapPut("/{id}",async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existingGame)
                    .CurrentValues
                    .SetValues(updatedGame.ToEntity(id));

            await dbContext.SaveChangesAsync();        
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
        });
        return group;
    }
}
