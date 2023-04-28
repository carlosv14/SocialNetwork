using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Api.Models;
using SocialNetwork.Api.Repositories;

namespace SocialNetwork.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IRepository<User> userRepository;

    public UsersController(IRepository<User> userRepository)
    {
        this.userRepository = userRepository;
    }

    //https://localhost:50670/users?username=a
    [HttpGet(Name = "GetUsers")]
    public IEnumerable<User> GetUsers([FromQuery] string? username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return userRepository.Get();
        }
        return userRepository.Filter(x => x.Username.StartsWith(username));
    }

    //https://localhost:50670/users/{id}
    [HttpGet("{id}")]
    public ActionResult GetUserById(int id)
    {
        var user = userRepository.GetById(id);
        if (user is null)
        {
            return BadRequest("No existe el usuario");
        }
        return Ok(user);
    }

    [HttpPost]
    public User CreateUser([FromBody]User user)
    {
        return userRepository.Add(user);
    }

    [HttpPut("{id}")]
    public User UpdateUser(int id, [FromBody]User user)
    {
        return userRepository.Update(user);
    }
}

