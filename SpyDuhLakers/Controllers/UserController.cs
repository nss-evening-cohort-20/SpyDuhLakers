using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuhLakers.Models;
using SpyDuhLakers.Repositories;

namespace SpyDuhLakers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(_userRepository.GetAllUsers());
    }
}
