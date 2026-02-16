namespace GameTournamentAPI.Models.GameDTOs
{
    public class GameResponeDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "";

        public DateTime Date { get; set; }

        public Guid TournamentId { get; set; }

    public static GameResponeDTO FromEntity (Game game)
        {
            return new GameResponeDTO
            {
                Id = game.Id,
                Title = game.Title,
                Date = game.Date,
                TournamentId = game.TournamentId
            };
        }
    }
}
