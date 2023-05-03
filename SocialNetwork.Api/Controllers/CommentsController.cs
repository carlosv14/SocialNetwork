using System;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Api.Models;
using SocialNetwork.Api.Repositories;

namespace SocialNetwork.Api.Controllers
{
	[ApiController]
	public class CommentsController : ControllerBase
	{
        private readonly IRepository<Comment> commentRepository;
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<User> userRepository;

        public CommentsController(
            IRepository<Comment> commentRepository,
            IRepository<Post> postRepository,
            IRepository<User> userRepository)
        {
            this.commentRepository = commentRepository;
            this.postRepository = postRepository;
            this.userRepository = userRepository;
        }

		[HttpPost("posts/{postId}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateComment([FromRoute]int postId, [FromBody]Comment comment)
		{
            var post = this.postRepository.GetById(postId);
            if (post is null)
            {
                return BadRequest($"No se encontró un post con id {postId} para agregar el comentario");
            }
            var user = this.userRepository.GetById(comment.UserId);
            if (user is null)
            {
                return BadRequest($"No se encontró un usuario con id {comment.UserId} para agregar el comentario");
            }

            if (string.IsNullOrEmpty(comment.Content))
            {
                return BadRequest("El comentario debe tener contenido.");
            }

            this.commentRepository.Add(comment);
            return new CreatedAtActionResult(nameof(GetCommentById), "Comments", new { postId = postId, commentId = comment.Id }, comment);
				
        }

		[HttpGet("posts/{postId}/comments/{commentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult GetCommentById([FromRoute]int postId, int commentId)
		{
            var post = this.postRepository.GetById(postId);
            if (post is null)
            {
                return BadRequest($"No se encontró un post con id {postId}");
            }

            var comment = this.commentRepository.GetById(commentId);
            if (comment is null)
            {
                return NotFound($"No se encontró un comentario con el id {postId}");
            }
            return Ok(comment);
        }
	}
}

