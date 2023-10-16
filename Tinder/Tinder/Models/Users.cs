namespace Tinder.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Age { get; set; }
        public string Hobby { get; set; }
        public string Role { get; set; }
        public string PhotoJson { get; set; }
        public string Email { get; set;}
        public string Password { get; set;}
        public virtual Locality? Locality { get; set;}
    }
}
