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
        public TournamentResponseDTO GetById(Guid id)
        {
            var tournament = _tournaments.FirstOrDefault(i => i.Id == id);
            return TournamentResponseDTO.FromEntity(tournament);
        }
        public Tournament? GetEntityById(Guid id)
        {
            return _tournaments.FirstOrDefault(t => t.Id == id);
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

        public bool Update(Guid id, TournamentUpdateDTO tournament)
        {
            var existingTournament = _tournaments.FirstOrDefault(t => t.Id == id);
            if (existingTournament == null) return false;


            existingTournament.Title = tournament.Title;
            existingTournament.Description = tournament.Description;
            existingTournament.MaxPlayers = tournament.MaxPlayers;
            existingTournament.Date = tournament.Date;
           

            return true;
        }

        public bool Delete(Guid id)
        {
            var tourToRemove = _tournaments.FirstOrDefault(t => t.Id == id);
            if (tourToRemove == null) return false;

            _tournaments.Remove(tourToRemove);
            return true;
        }
    }
}
