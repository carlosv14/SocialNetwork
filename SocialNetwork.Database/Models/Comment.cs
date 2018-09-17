using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Database.Models
{
    public class Comment
    {
        [Key]
        public long Id { get; set; }

        [StringLength(100)]
        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey(nameof(Post))]
        public long PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}