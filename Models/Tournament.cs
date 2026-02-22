namespace GameTournamentAPI.Models
{
    public class Tournament
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public int MaxPlayers { get; set; }

        public DateTime Date { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
