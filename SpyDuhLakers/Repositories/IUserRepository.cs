using SpyDuhLakers.Models;

namespace SpyDuhLakers.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetbyId(int Id);
        void Insert(User user);
        List<User> GetUserBySkill(string name);
    }
}