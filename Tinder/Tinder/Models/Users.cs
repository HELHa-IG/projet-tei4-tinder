using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Hobbys { get; set; }
        [Required]
        public string Role { get; set; }
        public string PhotoJson { get; set; }
        public string Email { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        public string Token { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public int LocalityId { get; set; }
        [ForeignKey("LocalityId")]
        public virtual Locality Locality { get; set; }

        [JsonIgnore]
        public ICollection<Questions>? Questions { get; set; }

        [JsonIgnore]
        public ICollection<MatchLike>? MatchLike01 { get; set; }

        [JsonIgnore]
        public ICollection<MatchLike>? MatchLike02 { get; set; }

        [JsonIgnore]
        public ICollection<Discussion>? Discussion01 { get; set; }

        [JsonIgnore]
        public ICollection<Discussion>? Discussion02 { get; set; }
    }
}
