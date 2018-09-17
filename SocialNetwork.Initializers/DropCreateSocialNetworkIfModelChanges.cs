using SocialNetwork.Database.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Initializers
{
    public class DropCreateSocialNetworkIfModelChanges : DropCreateDatabaseIfModelChanges<SocialNetworkContext>
    {
        protected override void Seed(SocialNetworkContext context)
        {
            var task = Task.Run(async () => await SocialNetworkSeed.GenerateTestData(context));
            task.Wait();
        }
    }
}
