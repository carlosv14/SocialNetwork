using SocialNetwork.Database.Contexts;
using SocialNetwork.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Initializers
{
    public class SocialNetworkSeed
    {
        public static async Task GenerateTestData(SocialNetworkContext socialNetworkContext)
        {
            var appUser = new ApplicationUser();
            appUser.UserName = "User1";
            appUser.Email = "user1@gmail.com";
            appUser.PasswordHash = "AGwu0Rve83bj8IDQ6TYxjEguZBtQfOCyOmJZfUAAg/lYiLCQasQPE9yp+pi/cIl5+w==";

            var appUser2 = new ApplicationUser();
            appUser2.UserName = "User2";
            appUser2.Email = "user2@gmail.com";
            appUser2.PasswordHash = "AGwu0Rve83bj8IDQ6TYxjEguZBtQfOCyOmJZfUAAg/lYiLCQasQPE9yp+pi/cIl5+w==";


            var appUser3 = new ApplicationUser();
            appUser3.UserName = "User3";
            appUser3.Email = "user3@gmail.com";
            appUser3.PasswordHash = "AGwu0Rve83bj8IDQ6TYxjEguZBtQfOCyOmJZfUAAg/lYiLCQasQPE9yp+pi/cIl5+w==";

            var appUser4 = new ApplicationUser();
            appUser4.UserName = "User4";
            appUser4.Email = "user4@gmail.com";
            appUser4.PasswordHash = "AGwu0Rve83bj8IDQ6TYxjEguZBtQfOCyOmJZfUAAg/lYiLCQasQPE9yp+pi/cIl5+w==";


            var appUser5 = new ApplicationUser();
            appUser5.UserName = "User5";
            appUser5.Email = "user5@gmail.com";
            appUser5.PasswordHash = "AGwu0Rve83bj8IDQ6TYxjEguZBtQfOCyOmJZfUAAg/lYiLCQasQPE9yp+pi/cIl5+w==";

            var appUser6 = new ApplicationUser();
            appUser6.UserName = "User6";
            appUser6.Email = "user6@gmail.com";
            appUser6.PasswordHash = "AGwu0Rve83bj8IDQ6TYxjEguZBtQfOCyOmJZfUAAg/lYiLCQasQPE9yp+pi/cIl5+w==";

            var appUser7 = new ApplicationUser();
            appUser7.UserName = "User7";
            appUser7.Email = "user7@gmail.com";
            appUser7.PasswordHash = "AGwu0Rve83bj8IDQ6TYxjEguZBtQfOCyOmJZfUAAg/lYiLCQasQPE9yp+pi/cIl5+w==";

            var user = new User
            {
                ApplicationUser = appUser,
                Posts = new List<Post>
                {
                     new Post
                     {
                         Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean purus urna, egestas nec aliquam in, tempus quis diam. Sed non odio a augue tempor ornare."
                     }
                },
                ProfilePictureUrl = "https://via.placeholder.com/150"
            };

            var user2 = new User
            {
                ApplicationUser = appUser2,
                Posts = new List<Post>
                {
                     new Post
                     {
                         Content = "Praesent luctus interdum diam placerat condimentum. Sed rutrum dapibus dui quis mattis. Donec id porta odio. Duis non consequat lorem.",
                         Comments = new List<Comment>()
                         {
                             new Comment
                             {
                                 Content = "Very cool!",
                                 User = user
                             }
                         }
                     }
                },
                ProfilePictureUrl= "https://via.placeholder.com/150"
            };

            var friendship = new Friendship
            {
                User1 = user,
                User2 = user2
            };


            var user3 = new User
            {
                ApplicationUser = appUser3,
                Posts = new List<Post>
                {
                     new Post
                     {
                         Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                     }
                },
                ProfilePictureUrl = "https://via.placeholder.com/150"
            };

            var user4 = new User
            {
                ApplicationUser = appUser4,
                Posts = new List<Post>
                {
                     new Post
                     {
                         Content = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                         Comments = new List<Comment>()
                         {
                             new Comment
                             {
                                 Content = "Not so cool!",
                                 User = user3
                             }
                         }
                     }
                },
                ProfilePictureUrl= "https://via.placeholder.com/150"
            };

            var friendship2 = new Friendship
            {
                User1 = user3,
                User2 = user4
            };


            var user6 = new User
            {
                ApplicationUser = appUser6,
                Posts = new List<Post>
                {
                     new Post
                     {
                         Content = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                     }
                },
                ProfilePictureUrl = "https://via.placeholder.com/150"
            };

            var user5 = new User
            {
                ApplicationUser = appUser5,
                Posts = new List<Post>
                {
                     new Post
                     {
                         Content = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                         Comments = new List<Comment>()
                         {
                             new Comment
                             {
                                 Content = "Kind of cool!",
                                 User = user6
                             }
                         }
                     }
                },
                ProfilePictureUrl = "https://via.placeholder.com/150"
            };


            var user7 = new User
            {
                ApplicationUser = appUser7,
                Posts = new List<Post>
                {
                     new Post
                     {
                         Content = " Etiam sit amet nisl purus in. Erat imperdiet sed euismod nisi porta.",
                         Comments = new List<Comment>()
                         {
                             new Comment
                             {
                                 Content = "Kind of cool!",
                                 User = user6
                             },
                             new Comment
                             {
                                 Content = "It's ok",
                                 User = user5
                             }
                         }
                     }
                },
                ProfilePictureUrl = "https://via.placeholder.com/150"
            };

            var friendship3 = new Friendship
            {
                User1 = user5,
                User2 = user6
            };

            var friendship4 = new Friendship
            {
                User1 = user5,
                User2 = user7
            };

            var friendship5 = new Friendship
            {
                User1 = user6,
                User2 = user7
            };

            socialNetworkContext.SocialNetworkUsers.Add(user);
            socialNetworkContext.SocialNetworkUsers.Add(user2);
            socialNetworkContext.SocialNetworkUsers.Add(user3);
            socialNetworkContext.SocialNetworkUsers.Add(user4);
            socialNetworkContext.SocialNetworkUsers.Add(user5);
            socialNetworkContext.SocialNetworkUsers.Add(user6);
            socialNetworkContext.SocialNetworkUsers.Add(user7);
            socialNetworkContext.Friendships.Add(friendship);
            socialNetworkContext.Friendships.Add(friendship2);
            socialNetworkContext.Friendships.Add(friendship3);
            socialNetworkContext.Friendships.Add(friendship4);
            socialNetworkContext.Friendships.Add(friendship5);
            await socialNetworkContext.SaveChangesAsync();

        }

    }
}
