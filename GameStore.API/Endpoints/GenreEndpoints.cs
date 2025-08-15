using System;
using GameStore.API.Data;
using GameStore.API.Mapping;
using GameStore.API.Dtos;
using Microsoft.EntityFrameworkCore;
namespace GameStore.API.Endpoints;

public static class GenreEndpoints
{
    public static RouteGroupBuilder MapGenreEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");
        group.MapGet("/", async (GameStoreContext dbContext) =>
                     await dbContext.Genres
                                    .Select(genre => genre.ToDto())
                                    .AsNoTracking()
                                    .ToListAsync());

        return group;                            
    }
}
