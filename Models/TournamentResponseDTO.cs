using GameTournamentAPI.Models;

namespace GameTournamentAPI.Models
{
    public class TournamentResponseDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "";

        public int MaxPlayers { get; set; }
        public DateTime Date { get; set; }
        public ICollection<GameDto> Games { get; set; } = new List<GameDto>();
        

        public static TournamentResponseDTO FromEntity (Tournament tournament)
        {
            return new TournamentResponseDTO
            {
                Id = tournament.Id,
                Title = tournament.Title,
                MaxPlayers = tournament.MaxPlayers,
                Date = tournament.Date,
                Games = tournament.Games.Select(g => new GameDto
                {
                    Id = g.Id,
                    Title = g.Title
                }).ToList()
            };
            }
        }

}