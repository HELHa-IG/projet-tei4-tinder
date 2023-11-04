using System.ComponentModel.DataAnnotations.Schema;

namespace Tinder.Models
{
    public class Questions
    {
        public int Id { get; set; }
        public string QuestionJson { get; set; }
        public int UserId { get; set; }
        [ForeignKey("userId")]

        public virtual Users User { get; set; }


    }
}
