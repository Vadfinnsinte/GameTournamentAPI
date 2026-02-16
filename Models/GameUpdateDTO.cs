using System.ComponentModel.DataAnnotations;

namespace GameTournamentAPI.Models
{
    public class GameUpdateDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title måste vara minst 3 tecken")]
        public string Title { get; set; } = "";

        [Required]
        public DateTime Date { get; set; }


    }
}
