using GameTournamentAPI.Converters;
using GameTournamentAPI.Models.GameDTOs;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace GameTournamentAPI.Models.TournamentDTOs
{
    public class TournamentResponseDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "";

        public int MaxPlayers { get; set; }


        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }
        public ICollection<GameDto> Games { get; set; } = new List<GameDto>();

        public static Expression<Func<Tournament, TournamentResponseDTO>> FromEntity =>
            t => new TournamentResponseDTO
            {
                Id = t.Id,
                Title = t.Title,
                MaxPlayers = t.MaxPlayers,
                Date = t.Date,
                Games = t.Games.Select(g => new GameDto
                {
                    Id = g.Id,
                    Title = g.Title,
                    Date = g.Date
                }).ToList()
            };
    }


}