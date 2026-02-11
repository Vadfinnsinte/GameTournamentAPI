namespace GameTournamentAPI.Models
{
    public class GameDto
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string Title { get; set; } = "";
        public GameDto() { }

        public GameDto(Game game)
        {
            Id = game.Id;
            Title = game.Title;
        }

    }
}
