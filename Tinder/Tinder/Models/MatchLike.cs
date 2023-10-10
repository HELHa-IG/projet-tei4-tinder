namespace Tinder.Models
{
    public class MatchLike
    {
        public int Id { get; set; }
        public string ScoreUser01 { get; set; }
        public string ScoreUser02 { get; set; }
        public Boolean User01Like { get; set;}
        public Boolean User02Like { get; set;}
        public string IdUser01 { get; set; }
        public string IdUser02 { get; set; }

    }
}
