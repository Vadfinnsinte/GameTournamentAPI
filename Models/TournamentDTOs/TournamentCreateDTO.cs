
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GameTournamentAPI.Converters;
using GameTournamentAPI.Models.Validation;

namespace GameTournamentAPI.Models.TournamentDTOs
{
    public class TournamentCreateDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title måste vara minst 3 tecken")]
        public string Title { get; set; } = "";
        [Required]
        [MaxLength(500, ErrorMessage = "Description får max vara 500 tecken.")]
        public string Description { get; set; } = "";
        [Required]
        [Range(2, int.MaxValue, ErrorMessage = "MaxPlayers must be at least 2.")]
        public int MaxPlayers { get; set; }
        [Required]
 
        [JsonConverter(typeof(DateTimeConverter))]
        [NotInPast]
        public DateTime Date { get; set; } 
    }
}
