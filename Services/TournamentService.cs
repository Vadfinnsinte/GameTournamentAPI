using GameTournamentAPI.Data;
using GameTournamentAPI.Models;
using GameTournamentAPI.Models.GameDTOs;
using GameTournamentAPI.Models.TournamentDTOs;
using Microsoft.EntityFrameworkCore;


namespace GameTournamentAPI.Services
{
    public class TournamentService
    {
        private readonly AppDbContext _context;
        public TournamentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task <IEnumerable<TournamentResponseDTO>> GetAllAsync()
        {
            return await _context.Tournaments
                .Include(t => t.Games)
                .Select(TournamentResponseDTO.FromEntity)
                .ToListAsync(); 
        }

        public async Task<TournamentResponseDTO?> GetByTitleAsync(string title)
        {
        
            return await _context.Tournaments
                .Include(t => t.Games)
                .Where(t => t.Title == title)
                .Select(TournamentResponseDTO.FromEntity)
                .FirstOrDefaultAsync();
        }
        public async Task<TournamentResponseDTO?> GetByIdAsync(Guid id)
        {
            return await _context.Tournaments
                .Include(t => t.Games)
                .Where(t => t.Id == id)
                .Select(TournamentResponseDTO.FromEntity)
                .FirstOrDefaultAsync();
           
        }

        public async Task<TournamentResponseDTO> CreateAsync(TournamentCreateDTO dto)
        {
            var tournament = new Tournament
            {
                Title = dto.Title,
                Description = dto.Description,
                MaxPlayers = dto.MaxPlayers,
                Date = dto.Date
            };
            _context.Tournaments.Add(tournament);

            await _context.SaveChangesAsync();

            return new TournamentResponseDTO
            {
                Id = tournament.Id,
                Title = tournament.Title,
                MaxPlayers = tournament.MaxPlayers,
                Date = tournament.Date,
                Games = new List<GameDto>()
            };

        }

        public async Task<bool> UpdateAsync(Guid id, TournamentUpdateDTO tournament)
        {
            var existingTournament = await _context.Tournaments.FindAsync(id);
            if (existingTournament == null) return false;


            existingTournament.Title = tournament.Title;
            existingTournament.Description = tournament.Description;
            existingTournament.MaxPlayers = tournament.MaxPlayers;
            existingTournament.Date = tournament.Date;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var tourToRemove = await _context.Tournaments.FindAsync(id);
            if (tourToRemove == null) return false;

            _context.Tournaments.Remove(tourToRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
