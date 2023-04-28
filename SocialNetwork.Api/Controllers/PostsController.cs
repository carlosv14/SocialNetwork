using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Api.Models;

namespace SocialNetwork.Api.Controllers
{
	[ApiController]
	public class PostsController : ControllerBase
	{
        private readonly SocialNetworkContext context;

        public PostsController(SocialNetworkContext context)
        {
            this.context = context;
        }

        //http://localhost:4000/posts/43jkgdsggsdj
        /// <summary>
        /// Agrega una publicación para el usuario.
        /// </summary>
        /// <param name="userId">El id del usuario que agregará la publicación.</param>
        /// <param name="post">La publicación a agregar.</param>
        /// <returns>La publicación agregada.</returns>
        [HttpPost("users/{userId}/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddPost([FromRoute]int userId, [FromBody]Post post)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == userId);
            if (user is null)
            {
                return BadRequest($"No se encontró un usuario con id {userId} para crear el post");
            }

            if (string.IsNullOrEmpty(post.Content))
            {
                return BadRequest("No se puede crear un post sin contenido.");
            }

            context.Posts.Add(post);
            context.SaveChanges();
            return new CreatedAtActionResult("GetPostById", "Posts", new { userId = userId, postId = post.Id }, post);
        }

        [HttpGet("users/{userId}/[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetPosts([FromRoute] int userId)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == userId);
            if (user is null)
            {
                return BadRequest($"No se encontró un usuario con id {userId}");
            }

            return Ok(context.Posts.Where(x => x.UserId == userId));
        }

        [HttpGet("users/{userId}/[controller]/{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetPostById([FromRoute] int userId, int postId)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == userId);
            if (user is null)
            {
                return BadRequest($"No se encontró un usuario con id {userId}");
            }

            var post = context.Posts.FirstOrDefault(x => x.UserId == userId && x.Id == postId);
            if (post is null)
            {
                return NotFound($"No se encontró un post con el id {postId}");
            }
            return Ok(post);
        }
    }
}

