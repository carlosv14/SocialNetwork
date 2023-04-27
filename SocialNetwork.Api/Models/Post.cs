using System;
namespace SocialNetwork.Api.Models
{
	public class Post
	{
		public int Id { get; set; }

		public string Content { get; set; }

		public int UserId { get; set; }

		public User User { get; set; }

		public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
	}
}

