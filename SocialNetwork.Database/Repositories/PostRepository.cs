using SocialNetwork.Database.Contexts;
using SocialNetwork.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Database.Repositories
{
    public class PostRepository : SocialNetworkBaseRepository<Post>
    {
        public PostRepository(SocialNetworkContext context) : base(context)
        {
        }

        public override IQueryable<Post> All()
        {
            return this.Context.Posts;
        }
    }
}
