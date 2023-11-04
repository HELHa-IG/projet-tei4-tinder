using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class Locality
    {
        public int Id { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        [JsonIgnore]
        public virtual List<Users>? Users { get; set; }
    }
}
