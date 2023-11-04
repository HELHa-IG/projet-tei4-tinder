using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class MatchLike
    {
        public int Id { get; set; }
        public string ScoreUser01 { get; set; }
        public string ScoreUser02 { get; set; }
        public Boolean User01Like { get; set;}
        public Boolean User02Like { get; set;}

        [ForeignKey("IdUser01")]
        public int IdUser01 { get; set; }

        [ForeignKey("IdUser02")]
        public int IdUser02 { get; set; }

        [JsonIgnore]
        public virtual Users? User02 { get; set; }

        [JsonIgnore]
        public virtual Users? User01 { get; set; }

    }
}
