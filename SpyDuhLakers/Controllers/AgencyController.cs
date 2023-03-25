﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuhLakers.Models;
using SpyDuhLakers.Repositories;

namespace SpyDuhLakers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgencyController : ControllerBase
{
    private readonly IAgencyRepository _agencyRepository;

    public AgencyController(IAgencyRepository AgencyRepository)
    {
        _agencyRepository = AgencyRepository;
    }

    [HttpGet]
    public IActionResult GetAllAgencies()
    {
        return Ok(_agencyRepository.GetAllAgencies());
    }
}

//    [HttpGet("skill/{skill}")]
//    public IActionResult GetSkill(string skill)
//    {
//        List<User> listOfSkills = _userRepository.GetUserBySkill(skill);

//        if (listOfSkills == null)
//        {
//            return NotFound();
//        }
//        return Ok(listOfSkills);
//    }

//    [HttpGet("{id}")]

//    public IActionResult GetUserById(int id)
//    {
//        var user = _userRepository.GetUserbyId(id);
//        if (user == null)
//        {
//            return NotFound();
//        }
//        return Ok(user);
//    }

//    [HttpPost]

//    public IActionResult AddUser(User user)
//    {
//        _userRepository.Insert(user);
//        return Created("/api/user/userSearch?id=" + user.Id, user);
//    }

//    [HttpGet("agency/{agency}")]
//    public IActionResult GetUserByAgency(string agency)
//    {
//        List<User> SpyAgency = _userRepository.GetUserByAgency(agency);

//        if (SpyAgency == null)
//        {
//            return NotFound();
//        }
//        return Ok(SpyAgency);
//    }
//}
