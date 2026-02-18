using GameTournamentAPI.Converters;
using GameTournamentAPI.Models.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameTournamentAPI.Models.GameDTOs
{
    public class GameUpdateDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title måste vara minst 3 tecken")]
        public string Title { get; set; } = "";

        [Required]

        [JsonConverter(typeof(DateTimeConverter))]
        [NotInPast]
        public DateTime Date { get; set; }


    }
}
