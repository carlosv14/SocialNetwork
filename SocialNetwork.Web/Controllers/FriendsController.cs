using Microsoft.AspNet.Identity;
using SocialNetwork.Database.Models;
using SocialNetwork.Database.Repositories;
using SocialNetwork.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SocialNetwork.Web.Controllers
{
    [Authorize]
    public class FriendsController : ApiController
    {
        private readonly IRepository<Friendship> friendshipRepository;
        private readonly IRepository<User> userRepository;

        public FriendsController(IRepository<Friendship> friendshipRepository, IRepository<User> userRepository)
        {
            this.friendshipRepository = friendshipRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<UserListItemViewModel> Get(string searchTerm)
        {
            var userId = Thread.CurrentPrincipal.Identity.GetUserId();

            var myFriends = this.friendshipRepository.All()
                .Where(x => x.User1Id == userId || x.User2Id == userId)
                .Select(x => x.User1Id == userId ? new { userId = x.User2Id } : new { userId = x.User1Id });

            var notFriends = this.userRepository.All()
                .GroupJoin(myFriends, u => u.UserId, mf => mf.userId, (u, mf) => new { User = u, IsFriend = mf.Any() });

            if (searchTerm == null || searchTerm == string.Empty)
            {
                return notFriends
                    .Where(x => !x.IsFriend && x.User.UserId != userId)
                    .Select(x => new UserListItemViewModel
                    {
                        Id = x.User.UserId,
                        UserName = x.User.ApplicationUser.UserName
                    });
            }

            return notFriends
                    .Where(x => !x.IsFriend && x.User.UserId != userId && (x.User.ApplicationUser.UserName.Contains(searchTerm) || searchTerm.Contains(x.User.ApplicationUser.UserName)))
                    .Select(x => new UserListItemViewModel
                    {
                        Id = x.User.UserId,
                        UserName = x.User.ApplicationUser.UserName
                    });
        }

        [HttpPost]
        [Route("api/Friends/Add/{userId}")]
        public async Task<IHttpActionResult> Add(string userId)
        {
            try
            {
                var currentUserId = Thread.CurrentPrincipal.Identity.GetUserId();
                var newFriend = await this.userRepository.FirstOrDefaultAsync(x => x.UserId == userId);
                if (newFriend == null)
                {
                    this.BadRequest("El usuario no existe");
                }
                this.friendshipRepository.Create(new Friendship
                {
                    User1Id = currentUserId,
                    User2Id = userId
                });

                await this.friendshipRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

            return this.Ok();
        }
    }
}