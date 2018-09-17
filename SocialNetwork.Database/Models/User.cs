using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Database.Models
{
    public class User
    {
        public User()
        {
            this.Posts = new HashSet<Post>();
        }

        public ICollection<Post> Posts { get; set; }

        [Key]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }
        
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required(AllowEmptyStrings =false)]
        public string ProfilePictureUrl { get; set; }
    }
}
