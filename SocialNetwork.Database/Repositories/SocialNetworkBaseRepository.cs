using SocialNetwork.Database.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Database.Repositories
{
    public abstract class SocialNetworkBaseRepository<TEntity> : BaseRepository<TEntity, SocialNetworkContext>
        where TEntity : class
    {
        protected SocialNetworkBaseRepository(SocialNetworkContext context)
            : base(context)
        {
            this.Context = context;
        }

    }
}
