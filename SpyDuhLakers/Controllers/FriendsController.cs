﻿using Microsoft.AspNetCore.Mvc;
using SpyDuhLakers.Models;
using SpyDuhLakers.Repositories;
using System.Collections.Generic;

namespace SpyDuhLakers.Controllers
{
    [Route("api/friends")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly FriendRepository _friendRepository;

        public FriendController(string connectionString)
        {
            _friendRepository = new FriendRepository(connectionString);
        }

        // GET api/friends
        [HttpGet]
        public ActionResult<IEnumerable<Friend>> Get()
        {
            return Ok(_friendRepository.GetAll());
        }

        // GET api/friends/5
        [HttpGet("{id}")]
        public ActionResult<Friend> Get(int id)
        {
            Friend friend = _friendRepository.GetById(id);
            if (friend == null)
            {
                return NotFound();
            }
            return Ok(friend);
        }

        // POST api/friends
        [HttpPost]
        public IActionResult Post([FromBody] Friend friend)
        {
            if (friend == null)
            {
                return BadRequest();
            }
            _friendRepository.Insert(friend);
            return Created("/api/friends/" + friend.Id, friend);
        }

        // PUT api/friends/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Friend friend)
        {
            if (friend == null || friend.Id != id)
            {
                return BadRequest();
            }

            Friend existingFriend = _friendRepository.GetById(id);
            if (existingFriend == null)
            {
                return NotFound();
            }

            existingFriend.userId = friend.userId;
            existingFriend.friendId = friend.friendId;

            _friendRepository.Update(existingFriend);

            return NoContent();
        }

        // DELETE api/friends/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Friend friend = _friendRepository.GetById(id);
            if (friend == null)
            {
                return NotFound();
            }

            _friendRepository.Delete(friend.Id);

            return NoContent();
        }
    }
}
