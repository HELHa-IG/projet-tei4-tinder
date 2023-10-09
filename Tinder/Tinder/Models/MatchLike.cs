namespace Tinder.Models
{
    public class MatchLike
    {
        public int Id { get; set; }
        int ScoreUser01 { get; set; }
        int ScoreUser02 { get; set; }
        bool UserLike01 { get; set; }
        bool UserLike02 { get; set; }

        int IdUser01 { get; set; }
        int IdUser02 { get; set; }
    }
}
