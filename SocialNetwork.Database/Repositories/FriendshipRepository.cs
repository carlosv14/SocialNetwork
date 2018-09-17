using SocialNetwork.Database.Contexts;
using SocialNetwork.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Database.Repositories
{
    public class FriendshipRepository : SocialNetworkBaseRepository<Friendship>
    {
        public FriendshipRepository(SocialNetworkContext context) : base(context)
        {
        }

        public override IQueryable<Friendship> All()
        {
            return this.Context.Friendships;
        }
    }
}
