using GameTournamentAPI.Converters;
using System.Text.Json.Serialization;

namespace GameTournamentAPI.Models.GameDTOs
{
    public class GameDto
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string Title { get; set; } = "";

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }
        public GameDto() { }

        public GameDto(Game game)
        {
            Id = game.Id;
            Title = game.Title;
            Date = game.Date;
        }

    }
}
