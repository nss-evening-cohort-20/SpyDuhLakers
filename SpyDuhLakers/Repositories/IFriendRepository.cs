using SpyDuhLakers.Models;

namespace SpyDuhLakers.Repositories
{
    public interface IFriendRepository
    {
        void Delete(int id);
        List<Friend> GetAll();
        Friend GetById(int Id);
        void Insert(Friend friend);
        void Update(Friend friend);
    }
}