namespace GameLibrary.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public int PublishYear { get; set; }
        public string Publisher { get; set; }
    }
}