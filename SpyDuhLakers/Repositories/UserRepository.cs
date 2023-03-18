using Microsoft.Data.SqlClient;
using SpyDuhLakers.Models;
using System;
using System.Reflection.Metadata.Ecma335;

namespace SpyDuhLakers.Repositories
{

    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public List<User> GetAll()
        {
            using (SqlConnection conn = Connection)

            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name FROM Users";

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<User> chores = new List<User>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");

                        int idValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("Name");
                        string nameValue = reader.GetString(nameColumnPosition);

                        User User = new User()
                        {
                            Id = idValue,
                            Name = nameValue
                        };
                        chores.Add(User);
                    }
                    reader.Close();

                    return chores;

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