using GameTournamentAPI.Models;
using GameTournamentAPI.Models.GameDTOs;

namespace GameTournamentAPI.Services
{
    public class GameService
    {
        private readonly List<Game> _games = new();
        private readonly TournamentService _tournamentService;

        public GameService(TournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        public IEnumerable<GameResponeDTO> GetAll()
        {
            return _games.Select(GameResponeDTO.FromEntity);
        }

        public GameResponeDTO? GetByTitle(string title)
        {
            var game = _games.FirstOrDefault(t => t.Title == title);
            if (game == null) return null;

            return GameResponeDTO.FromEntity(game);
        }
        public GameResponeDTO GetById(Guid id)
        {
            var game = _games.FirstOrDefault(i => i.Id == id);
            return GameResponeDTO.FromEntity(game);
        }

        public GameResponeDTO? Create(GameCreateDTO dto)
        {
            var tournament = _tournamentService.GetEntityById(dto.TournamentId);

            if (tournament == null)
                return null;

            var game = new Game
            {
                Title = dto.Title,
                TournamentId = dto.TournamentId,
                Date = dto.Date,
                Tournament = tournament
            };
            _games.Add(game);
            tournament.Games.Add(game);
            return GameResponeDTO.FromEntity(game);

        }

        public bool Update(Guid id, GameUpdateDTO game)
        {
            var existingGames = _games.FirstOrDefault(t => t.Id == id);
            if (existingGames == null) return false;

            existingGames.Title = game.Title;
            existingGames.Date = game.Date;

            return true;
        }

        public bool Delete(Guid id)
        {
            var gameToRemove = _games.FirstOrDefault(t => t.Id == id);
            if (gameToRemove == null) return false;

            _games.Remove(gameToRemove);
            return true;
        }
    }
}

