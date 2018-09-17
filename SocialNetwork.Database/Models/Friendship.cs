using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Database.Models
{
    public class Friendship
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(User1))]
        public string User1Id { get; set; }

        [ForeignKey(nameof(User2))]
        public string User2Id { get; set; }
       
        public virtual User User1 { get; set; }

        public virtual User User2 { get; set; }
    }
}