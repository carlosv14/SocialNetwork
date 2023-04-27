using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Api.Models;

namespace SocialNetwork.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    //https://localhost:50670/users?username=a
    [HttpGet(Name = "GetUsers")]
    public IEnumerable<User> GetUsers([FromQuery] string? username)
    {
        var builder = new DbContextOptionsBuilder<SocialNetworkContext>()
               .UseSqlite("DataSource=socialnetwork.db")
               .EnableSensitiveDataLogging()
               .LogTo(message => Debug.Write(message));
        var context = new SocialNetworkContext(builder.Options);
        context.Database.EnsureCreated();
        if (string.IsNullOrEmpty(username))
        {
            return context.Users;
        }
        return context.Users.Where(x => x.Username.StartsWith(username));
    }

    //https://localhost:50670/users/{id}
    [HttpGet("{id}")]
    public User GetUserById(int id)
    {
        var builder = new DbContextOptionsBuilder<SocialNetworkContext>()
               .UseSqlite("DataSource=socialnetwork.db");
        var context = new SocialNetworkContext(builder.Options);
        context.Database.EnsureCreated();
        return context.Users.First(x => x.Id == id);
    }

    [HttpPost]
    public User CreateUser([FromBody]User user)
    {
        var builder = new DbContextOptionsBuilder<SocialNetworkContext>()
               .UseSqlite("DataSource=socialnetwork.db");
        var context = new SocialNetworkContext(builder.Options);
        context.Database.EnsureCreated();
        context.Users.Add(user);
        return user;
    }

    [HttpPut("{id}")]
    public User UpdateUser(int id, [FromBody]User user)
    {
        var builder = new DbContextOptionsBuilder<SocialNetworkContext>()
               .UseSqlite("DataSource=socialnetwork.db");
        var context = new SocialNetworkContext(builder.Options);
        context.Database.EnsureCreated();
        var userToRemove = context.Users.First(x => x.Id == id);
        context.Users.Remove(userToRemove);
        context.Users.Add(user);
        return user;
    }
}

