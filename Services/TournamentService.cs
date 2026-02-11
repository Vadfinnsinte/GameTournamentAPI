using GameTournamentAPI.Models;

namespace GameTournamentAPI.Services
{
    public class TournamentService
    {
        private readonly List<Tournament> _tournaments = new();

        public IEnumerable<TournamentResponseDTO> GetAll()
        {
            return _tournaments.Select(TournamentResponseDTO.FromEntity);
        }

        public TournamentResponseDTO? GetByTitle(string title)
        {
            var tournament = _tournaments.FirstOrDefault(t => t.Title == title);
            if (tournament == null) return null;

            return TournamentResponseDTO.FromEntity(tournament);
        }
        public TournamentResponseDTO Create(TournamentCreateDTO dto)
        {
            var tournament = new Tournament
            {
                Title = dto.Title,
                Description = dto.Description,
                MaxPlayers = dto.MaxPlayers,
                Date = dto.Date
            };
            _tournaments.Add(tournament);

            return TournamentResponseDTO.FromEntity(tournament);




        }

    }
}
