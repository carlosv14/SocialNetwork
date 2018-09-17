using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Database.Models
{
    public class Reaction
    {
        [Key]
        public long Id { get; set; }

        public ReactionType ReactionType { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey(nameof(Post))]
        public long PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}