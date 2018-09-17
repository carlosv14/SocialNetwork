using SocialNetwork.Database.Contexts;
using SocialNetwork.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Database.Repositories
{
    public class SocialNetworkUserRepository : SocialNetworkBaseRepository<User>
    {
        public SocialNetworkUserRepository(SocialNetworkContext context) 
            : base(context)
        {
        }

        public override IQueryable<User> All()
        {
            return this.Context.SocialNetworkUsers;
        }
    }
}
