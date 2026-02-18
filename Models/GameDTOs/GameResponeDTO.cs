using GameTournamentAPI.Converters;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace GameTournamentAPI.Models.GameDTOs
{
    public class GameResponseDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "";

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }

        public Guid TournamentId { get; set; }

        public static Expression<Func<Game, GameResponseDTO>> FromEntity =>
            g => new GameResponseDTO
            {
                Id = g.Id,
                Title = g.Title,
                Date = g.Date,
                TournamentId = g.TournamentId
            };
    }
}
