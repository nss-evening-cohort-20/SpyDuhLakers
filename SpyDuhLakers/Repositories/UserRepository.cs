using Microsoft.Data.SqlClient;
using SpyDuhLakers.Models;
using SpyDuhLakers.Utils;
using System;
using System.Reflection.Metadata.Ecma335;

namespace SpyDuhLakers.Repositories
{

    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public List<User> GetAllUsers()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT
                        u.id AS SpyId,
                        u.name AS SpyName,
                        e.enemyId AS EnemyUserId,
                        e.id AS EnemyTableId,
                        enemy.name AS EnemyName,
                        f.id AS FriendTableId,
                        f.friendId AS FriendUserId,
                        friend.name AS FriendName,
                        s.id AS SkillTableId,
                        s.name AS SkillName,
                        sv.id AS ServiceTableId,
                        sv.name AS ServiceName,
                        sv.userId ServiceUserId
                    FROM Users u
                        LEFT JOIN Enemies e on u.id = e.userId
                        LEFT JOIN Friends f on u.id = f.userId
                        LEFT JOIN Skills s on u.id = s.userId
                        LEFT JOIN Services sv on u.id = sv.userId
                        LEFT JOIN Users enemy on enemy.id = e.enemyId
                        LEFT JOIN Users friend on friend.id = f.friendId";
                
                    var reader = cmd.ExecuteReader();

                    var users = new List<User>();

                    User user = null;

                    while (reader.Read())
                    {
                        user = new User()
                        {
                            Id = DbUtils.GetInt(reader, "SpyId"),
                            Name = DbUtils.GetString(reader, "SpyName"),
                            Enemies = new List<Enemy>(),
                            Friends = new List<Friend>(),
                            Skills = new List<Skill>(),
                            Services = new List<Service>()
                        };
                        
                        users.Add(user);

                        if(DbUtils.IsNotDbNull(reader, "EnemyUserId"))
                        {
                            user.Enemies.Add(new Enemy()
                            {
                                Id = DbUtils.GetInt(reader, "EnemyTableId"),
                                userId = DbUtils.GetInt(reader, "SpyId"),
                                enemyId = DbUtils.GetInt(reader, "EnemyUserId")
                            });
                        }

                        if(DbUtils.IsNotDbNull(reader, "FriendUserId"))
                        {
                            user.Friends.Add(new Friend()
                            {
                                Id = DbUtils.GetInt(reader, "FriendTableId"),
                                userId = DbUtils.GetInt(reader, "SpyId"),
                                friendId = DbUtils.GetInt(reader, "FriendUserId")
                            });
                        }

                        if(DbUtils.IsNotDbNull(reader, "SkillTableId"))
                        {
                            user.Skills.Add(new Skill()
                            {
                                Id = DbUtils.GetInt(reader, "SkillTableId"),
                                Name = DbUtils.GetString(reader, "SkillName")
                            });
                        }

                        if(DbUtils.IsNotDbNull(reader, "ServiceTableId"))
                        {
                            user.Services.Add(new Service()
                            {
                                Id = DbUtils.GetInt(reader, "ServiceTableId"),
                                Name = DbUtils.GetString(reader, "ServiceName"),
                                UserId = DbUtils.GetInt(reader, "ServiceUserId")
                            });
                        }
                    }

                    reader.Close();

                    return users;
                }
            }
        }

        public User GetbyId(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Name From User WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", Id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    User user = null;
                    if (reader.Read())
                    {
                        user = new User()
                        {
                            Id = Id,
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        };
                    }
                    reader.Close();
                    return user;
                }
            }
        }


        public void Insert(User user)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO User (Name}
                                        OUTPUT INSERTED.Id
                                        VALUES (@name)";
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    int id = (int)cmd.ExecuteScalar();



                }
            }
        }
    }
}