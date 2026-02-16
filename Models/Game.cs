using System.Reflection;

namespace GameTournamentAPI.Models
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "";

        public DateTime dateTime { get; set; }

        public Guid TournamentId { get; set; }

        public Tournament Tournament { get; set; } = null!;

    }
}
