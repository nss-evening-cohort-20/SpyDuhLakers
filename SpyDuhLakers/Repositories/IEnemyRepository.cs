using SpyDuhLakers.Models;

namespace SpyDuhLakers.Repositories
{
    public interface IEnemyRepository
    {
        void Delete(int id);
        List<Enemy> GetAll();
        Enemy GetById(int Id);
        void Insert(Enemy enemy);
        void Update(Enemy enemy);
    }
}