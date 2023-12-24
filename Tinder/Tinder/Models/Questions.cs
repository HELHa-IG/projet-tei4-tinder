using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class Questions
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Good { get; set; }
        public string Bad { get; set; }

        [ForeignKey("IdUser")]
        public int IdUser { get; set; }

        [JsonIgnore]
        public virtual Users? User { get; set; }
    }

}
