using Microsoft.AspNet.Identity;
using SocialNetwork.Database.Models;
using SocialNetwork.Database.Repositories;
using SocialNetwork.Web.Models;
using System.Collections.Generic;
using System.Threading;
using System.Web.Http;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SocialNetwork.Web.Controllers
{
    [Authorize]
    public class PostsController : ApiController
    {
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<Friendship> friendshipRepository;

        public PostsController(IRepository<Post> postRepository, IRepository<Friendship> friendshipRepository)
        {
            this.postRepository = postRepository;
            this.friendshipRepository = friendshipRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<PostListItemViewModel>> Get()
        {
            var userId = Thread.CurrentPrincipal.Identity.GetUserId();
            var myFriends = this.friendshipRepository.All()
                .Where(x => x.User1Id == userId || x.User2Id == userId)
                .Select(x => x.User1Id == userId ? new { userId = x.User2Id } : new { userId = x.User1Id });

            var posts = await this.postRepository.All()
                .Join(myFriends, p => p.UserId, mf => mf.userId, (p, mf) => p)
                .Union(this.postRepository.All().Where(x => x.UserId == userId))
                .ToListAsync();

            return posts.Select(x => new PostListItemViewModel
            {
                Content = x.Content,
                UserName = x.User.ApplicationUser.UserName,
                Id = x.Id
            });
        }

        public async Task<PostDetailViewModel> Get(long id)
        {
            var userId = Thread.CurrentPrincipal.Identity.GetUserId();
            var myFriends = this.friendshipRepository.All()
                .Where(x => x.User1Id == userId || x.User2Id == userId)
                .Select(x => x.User1Id == userId ? new { userId = x.User2Id } : new { userId = x.User1Id });

            var posts = await this.postRepository.All()
                .Join(myFriends, p => p.UserId, mf => mf.userId, (p, mf) => p)
                .Union(this.postRepository.All().Where(x => x.UserId == userId))
                .ToListAsync();

            var post = posts.FirstOrDefault(x => x.Id == id);
            if (post == null)
            {
                throw new System.Exception("Este post no es de un amigo");
            }

            return new PostDetailViewModel
            {
                Comments = post.Comments.Select(x => new CommentListItemViewModel { Content = x.Content, UserName = x.User.ApplicationUser.UserName }),
                Content = post.Content,
                UserName = post.User.ApplicationUser.UserName,
            };

        }

    }
}