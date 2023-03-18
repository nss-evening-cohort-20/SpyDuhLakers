using Microsoft.AspNetCore.Mvc;
using SpyDuhLakers.Models;
using SpyDuhLakers.Repositories;
using System.Collections.Generic;

namespace SpyDuhLakers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnemiesController : ControllerBase
    {
        private readonly IEnemyRepository _enemyRepository;

        public EnemiesController(IEnemyRepository enemyRepository)
        {
            _enemyRepository = enemyRepository;
        }

        [HttpGet]
        public IActionResult GetAllEnemies()
        {
            return Ok(_enemyRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetEnemyById(int id)
        {
            var enemy = _enemyRepository.GetById(id);
            if (enemy == null)
            {
                return NotFound();
            }
            return Ok(enemy);
        }

        [HttpPost]
        public IActionResult AddEnemy(Enemy enemy)
        {
            _enemyRepository.Insert(enemy);
            return CreatedAtAction("GetCreated", new { id = enemy.Id }, enemy);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEnemy(int id, Enemy enemy)
        {
            if (id != enemy.Id)
            {
                return BadRequest();
            }
            _enemyRepository.Update(enemy);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEnemy(int id)
        {
            var enemy = _enemyRepository.GetById(id);
            if (enemy == null)
            {
                return NotFound();
            }
            _enemyRepository.Delete(enemy.Id);
            return NoContent();
        }
    }
}
