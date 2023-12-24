using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class Discussion
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string dates { get; set; }

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
