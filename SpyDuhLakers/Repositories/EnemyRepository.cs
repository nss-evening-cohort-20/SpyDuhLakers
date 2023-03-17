using Microsoft.Data.SqlClient;
using SpyDuhLakers.Models;
using System.Diagnostics.Metrics;

namespace SpyDuhLakers.Repositories
{



    public class EnemyRepository : BaseRepository
    {
        public EnemyRepository(string connectionString) : base(connectionString) { }
        public List<Enemy> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select 
                                        id
                                        ,userId
                                        ,enemyId
                                        From [Enemies]";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Enemy> enemies = new List<Enemy>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);
                        int userIdColumnPosition = reader.GetOrdinal("userId");
                        int useridValue = reader.GetInt32(userIdColumnPosition);
                        int enemyIdColumnPosition = reader.GetOrdinal("enemyId");
                        int enemyidValue = reader.GetInt32(enemyIdColumnPosition);

                        Enemy Enemy = new Enemy()
                        {
                            Id = idValue,
                            userId = useridValue,
                            enemyId = enemyidValue,
                        };

                        enemies.Add(Enemy);
                    }
                    reader.Close();
                    return enemies;
                }
            }
        }


        public Enemy GetById(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, UserId, enemyId FROM Enemies WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", Id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Enemy enemy = null;
                    if (reader.Read())
                    {
                        enemy = new Enemy()
                        {
                            Id = Id,
                            userId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            enemyId = reader.GetInt32(reader.GetOrdinal("enemyId"))
                        };
                    }
                    reader.Close();
                    return enemy;
                }
            }
        }


        public void Insert(Enemy enemy)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Enemies (userId, enemyId) 
                                OUTPUT INSERTED.Id 
                                VALUES (@userId, @enemyId)";
                    cmd.Parameters.AddWithValue("@userId", enemy.userId);
                    cmd.Parameters.AddWithValue("@enemyId", enemy.enemyId);
                    int id = (int)cmd.ExecuteScalar();
                }
                conn.Close();
            }
        }

        public void Update(Enemy enemy)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Enemies SET 
                                    userId = @userId,
                                    enemyId = @enemyId
                                WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@userId", enemy.userId);
                    cmd.Parameters.AddWithValue("@enemyId", enemy.enemyId);
                    cmd.Parameters.AddWithValue("@id", enemy.Id);
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
                    cmd.CommandText = @"DELETE FROM Enemies WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }





    }

}
