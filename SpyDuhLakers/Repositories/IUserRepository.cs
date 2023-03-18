using SpyDuhLakers.Models;

namespace SpyDuhLakers.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetbyId(int Id);
        void Insert(User user);
    }
}