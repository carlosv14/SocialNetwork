using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Database.Models
{
    public class Post
    {

        public Post()
        {
            this.Comments = new HashSet<Comment>();
            this.Reactions = new HashSet<Reaction>();
        }

        [Key]
        public long Id { get; set; }

        [StringLength(200)]
        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Reaction> Reactions { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}