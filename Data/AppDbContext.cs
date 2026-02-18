using GameTournamentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameTournamentAPI.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Tournament> Tournaments => Set<Tournament>();
        public DbSet<Game> Games => Set<Game>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
