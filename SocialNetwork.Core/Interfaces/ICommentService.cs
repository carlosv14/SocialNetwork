using System;
using SocialNetwork.Core.Entities;

namespace SocialNetwork.Core.Interfaces
{
	public interface ICommentService
	{
		OperationResult<Comment> Create(Comment comment);

		OperationResult<Comment> GetById(int id, int postId);
	}
}

