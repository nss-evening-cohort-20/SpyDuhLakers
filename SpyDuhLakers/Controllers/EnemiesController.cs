using Microsoft.AspNetCore.Mvc;
using SpyDuhLakers.Models;
using SpyDuhLakers.Repositories;
using System.Collections.Generic;

namespace SpyDuhLakers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnemiesController : ControllerBase
    {
        private readonly EnemyRepository _enemyRepository;

        public EnemiesController(EnemyRepository enemyRepository)
        {
            _enemyRepository = enemyRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Enemy>> GetAllEnemies()
        {
            var enemies = _enemyRepository.GetAll();
            return Ok(enemies);
        }

        [HttpGet("{id}")]
        public ActionResult<Enemy> GetEnemyById(int id)
        {
            var enemy = _enemyRepository.GetById(id);
            if (enemy == null)
            {
                return NotFound();
            }
            return Ok(enemy);
        }

        [HttpPost]
        public ActionResult<Enemy> AddEnemy(Enemy enemy)
        {
            _enemyRepository.Insert(enemy);
            return CreatedAtAction(nameof(GetEnemyById), new { id = enemy.Id }, enemy);
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
