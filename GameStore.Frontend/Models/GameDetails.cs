using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Frontend.Models;

public class GameDetails
{

    public int Id { get; set; }
    public required string Name { get; set; }
    public string? GenreId { get; set; }

    [Range(0, 10)]
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
