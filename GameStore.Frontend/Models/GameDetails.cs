using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameStore.Frontend.Models;

public class GameDetails
{

    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    [Required(ErrorMessage = "The Genre Field is required")]
    // [JsonConverter(typeof(StringConverter))]
    public int GenreId { get; set; }
    [Required]
    [Range(0, 100)]
    public decimal Price { get; set; }
    [Required]
    public DateOnly ReleaseDate { get; set; }
}
