using Microsoft.Data.SqlClient;
using SpyDuhLakers.Models;
using SpyDuhLakers.Utils;
using System.Diagnostics.Metrics;

namespace SpyDuhLakers.Repositories
{
    public class FriendRepository : BaseRepository
    {
        public FriendRepository(string connectionString) : base(connectionString) { }
        public List<Friend> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, userId, friendId FROM [Friends]";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Friend> friends = new List<Friend>();

                    while (reader.Read())
                    {
                        friends.Add(new Friend()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            userId = DbUtils.GetInt(reader, "userId"),
                            friendId = DbUtils.GetInt(reader, "friendId"),
                        });
                       
                    }
                    reader.Close();
                    return friends;
                }
            }
        }


        public Friend GetById(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, UserId, FriendId FROM Friends WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", Id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Friend friend = null;
                    if (reader.Read())
                    {
                        friend = new Friend()
                        {
                            Id = Id,
                            userId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            friendId = reader.GetInt32(reader.GetOrdinal("FriendId"))
                        };
                    }
                    reader.Close();
                    return friend;
                }
            }
        }


        public void Update(Friend friend)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Friends SET 
                                    userId = @userId,
                                    friendId = @friendId
                                WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@userId", friend.userId);
                    cmd.Parameters.AddWithValue("@enemyId", friend.friendId);
                    cmd.Parameters.AddWithValue("@id", friend.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }


        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM FRIENDS WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }


        public void Insert(Friend friend)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Friends (userId, friendId) 
                                OUTPUT INSERTED.Id 
                                VALUES (@userId, @friendId)";
                    cmd.Parameters.AddWithValue("@userId", friend.userId);
                    cmd.Parameters.AddWithValue("@friendId", friend.friendId);
                    int id = (int)cmd.ExecuteScalar();
                }
                conn.Close();
            }
        }
    }
}
