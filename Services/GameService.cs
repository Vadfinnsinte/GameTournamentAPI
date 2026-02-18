using GameTournamentAPI.Data;
using GameTournamentAPI.Models;
using GameTournamentAPI.Models.GameDTOs;
using Microsoft.EntityFrameworkCore;

namespace GameTournamentAPI.Services
{
    public class GameService
    {
        private readonly AppDbContext _context;
   

        public GameService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<GameResponseDTO>>GetAllAsync()
        {
            return  await _context.Games
                .Select(GameResponseDTO.FromEntity)
                .ToListAsync();
        }

        public async Task<GameResponseDTO?> GetByTitleAsync(string title)
        {
            return await _context.Games
                .Where(g => g.Title == title)
                .Select(GameResponseDTO.FromEntity)
                .FirstOrDefaultAsync();
           
        }
        public async Task<GameResponseDTO?> GetByIdAsync(Guid id)
        {
            return await _context.Games
                .Where(g => g.Id == id)
                .Select(GameResponseDTO.FromEntity)
                .FirstOrDefaultAsync();
        }

        public async Task<GameResponseDTO?> CreateAsync(GameCreateDTO dto)
        {
            var tournament = await _context.Tournaments
            .FirstOrDefaultAsync(t => t.Id == dto.TournamentId);

            if (tournament == null) return null;

            if (dto.Date < tournament.Date.AddMinutes(10))
                throw new ArgumentException("Game must start at least 10 minutes after the tournament's start.");

            var game = new Game
            {
                Title = dto.Title,
                TournamentId = dto.TournamentId,
                Date = dto.Date,
           
            };
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return new GameResponseDTO
            {
                Id = game.Id,
                Title = game.Title,
                Date = game.Date,
                TournamentId = game.TournamentId
            };

        }

        public async Task<bool> UpdateAsync(Guid id, GameUpdateDTO game)
        {
            var existingGame = await _context.Games.FindAsync(id);
            if (existingGame == null) return false;

            existingGame.Title = game.Title;
            existingGame.Date = game.Date;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var gameToRemove = await _context.Games.FindAsync(id);
            if (gameToRemove == null) return false;

            _context.Remove(gameToRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

