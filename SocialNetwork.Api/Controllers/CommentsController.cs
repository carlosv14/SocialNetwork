using System;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Api.Models;

namespace SocialNetwork.Api.Controllers
{
	[ApiController]
	public class CommentsController : ControllerBase
	{
        private readonly SocialNetworkContext context;

        public CommentsController(SocialNetworkContext socialNetworkContext)
        {
            this.context = socialNetworkContext;
        }

		[HttpPost("posts/{postId}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateComment([FromRoute]int postId, [FromBody]Comment comment)
		{
            var post = context.Posts.FirstOrDefault(x => x.Id == postId);
            if (post is null)
            {
                return BadRequest($"No se encontró un post con id {postId} para agregar el comentario");
            }
            var user = context.Users.FirstOrDefault(x => x.Id == comment.UserId);
            if (user is null)
            {
                return BadRequest($"No se encontró un usuario con id {comment.UserId} para agregar el comentario");
            }

            if (string.IsNullOrEmpty(comment.Content))
            {
                return BadRequest("El comentario debe tener contenido.");
            }

            context.Comments.Add(comment);
            context.SaveChanges();
            return new CreatedAtActionResult(nameof(GetCommentById), "Comments", new { postId = postId, commentId = comment.Id }, comment);
				
        }

		[HttpGet("posts/{postId}/comments/{commentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult GetCommentById([FromRoute]int postId, int commentId)
		{
            var post = context.Posts.FirstOrDefault(x => x.Id == postId);
            if (post is null)
            {
                return BadRequest($"No se encontró un post con id {postId}");
            }

            var comment = context.Comments.FirstOrDefault(x => x.Id == commentId && x.PostId == postId);
            if (comment is null)
            {
                return NotFound($"No se encontró un comentario con el id {postId}");
            }
            return Ok(comment);
        }
	}
}

